using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;
using System;

public class Finish : MonoBehaviour
{
    [SerializeField] ItemCollector item;
    [SerializeField] GameObject WinMenu;
    [SerializeField] Chronometer chronometer;
    [SerializeField] TextMeshProUGUI time;

    [SerializeField] Transform scrollViewContent;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject nextLevelButton;
    public int lastLevel;



    public IEnumerator SubmitScore(int score){
        bool done = false;
        LootLockerSDKManager.SubmitScore("PlayerID",score,this.leaderBoardID(),(response) => {
            if(response.success){
                done = true;
                Debug.Log("Submitted");
            } else {
                done = true;
                Debug.Log("Not Submitted");
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public int leaderBoardID(){
        if(SceneManager.GetActiveScene().buildIndex == 4) return 9290;
        else if(SceneManager.GetActiveScene().buildIndex == 5) return 9291;
        else if(SceneManager.GetActiveScene().buildIndex == 6) return 9292;
        else return 9293;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTrigger");
        if(collision.gameObject.name == "PlayerCharacter" && item.collected){
            this.CompleteLevel();
        }
    }

    IEnumerator SetupRoutine(int score){
        yield return this.SubmitScore(score);
        yield return this.showLeaderBoard();
    }

    public void CompleteLevel()
    {
        var time = chronometer.time;
        int minutes= Mathf.FloorToInt(time / 60);
        float seconds= time % 60;
        this.time.SetText(minutes.ToString("D2") + ":" + seconds.ToString("00.000"));

        Time.timeScale = 0f;
        WinMenu.SetActive(true);

        Debug.Log(SceneManager.GetActiveScene().buildIndex.ToString());
        if(SceneManager.GetActiveScene().buildIndex == this.lastLevel){
            nextLevelButton.SetActive(false);
            Debug.Log("Disabled");
        }

        /*
        float record = (chronometer.time * 1000);
        TimeSpan t = TimeSpan.FromMilliseconds(chronometer.time);
        this.time.SetText(string.Format("{0:D2}:{1:D2}.{2:D2}",t.Minutes,t.Seconds,t.Milliseconds));
        */

        StartCoroutine(this.SetupRoutine((int) (chronometer.time * 1000)));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public IEnumerator showLeaderBoard(){
        yield return this.FetchTopHighScoresRoutine();
    }

    public IEnumerator FetchTopHighScoresRoutine(){
        bool done = false;
        var window= this.scrollViewContent.GetComponent<RectTransform>();
        TimeSpan t;
        LootLockerSDKManager.GetScoreList(this.leaderBoardID(),100,0,(response) => {
            if(response.success){
                LootLockerLeaderboardMember[] members = response.items;
                window.sizeDelta= new Vector2(window.sizeDelta.x,70*members.Length);
                for(int i = 0;i < members.Length;i++){
                    GameObject UserScore = Instantiate(prefab,scrollViewContent);
                    TextMeshProUGUI[] infos = UserScore.GetComponentsInChildren<TextMeshProUGUI>();
                    infos[0].SetText((i + 1).ToString());
                    infos[1].SetText(members[i].player.name);
                    t = TimeSpan.FromMilliseconds(members[i].score);
                    infos[2].SetText(string.Format("{0:D2}:{1:D2}.{2:D3}",t.Minutes,t.Seconds,t.Milliseconds));
                }
                done = true;
            } else {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}

