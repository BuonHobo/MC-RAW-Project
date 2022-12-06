using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;
using UnityEngine.UI;
using System;

public class LeaderBoardMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI LevelIndicator;
    [SerializeField] GameObject PreviousLeaderBoardButton;
    [SerializeField] GameObject NextLeaderBoardButton;
    [SerializeField] GameObject PosButton;
    [SerializeField] GameObject UserButton;
    [SerializeField] GameObject TimeButton;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] GameObject prefab;

    private List<GameObject> dynamicScore;
    private int currentLevel;
    public int maxLevel;
    public int minLevel;
    void Start()
    {
        this.dynamicScore = new List<GameObject>();
        this.currentLevel = 1;
        checkLevel();
        StartCoroutine(this.showLeaderBoard());
    }

    public void PreviousLeaderBoard(){
        this.currentLevel -= 1;
        StartCoroutine(this.showLeaderBoard());
    }

    public void NextLeaderBoard(){
        this.currentLevel += 1;
        StartCoroutine(this.showLeaderBoard());
    }

    void checkLevel(){
        this.LevelIndicator.SetText(this.currentLevel.ToString());
        if(this.currentLevel == this.minLevel){
            this.PreviousLeaderBoardButton.SetActive(false);
        } else {
            this.PreviousLeaderBoardButton.SetActive(true);
        }

        if(this.currentLevel == this.maxLevel){
            this.NextLeaderBoardButton.SetActive(false);
        } else {
            this.NextLeaderBoardButton.SetActive(true);
        }
    }

    public int getLeaderBoardID(){
        if(this.currentLevel == 1) return 9290;
        else if(this.currentLevel == 2) return 9291;
        else if(this.currentLevel == 3) return 9292;
        else if(this.currentLevel == 4) return 9293;
        else return 0;
    }

    public IEnumerator FetchTopHighScoresRoutine(){
        this.checkLevel();
        bool done = false;
        TimeSpan t;
        var window= this.scrollViewContent.GetComponent<RectTransform>();
        LootLockerSDKManager.GetScoreList(this.getLeaderBoardID(),100,0,(response) => {
            foreach(GameObject score in this.dynamicScore) Destroy(score);
            if(response.success){
                LootLockerLeaderboardMember[] members = response.items;
                window.sizeDelta= new Vector2(window.sizeDelta.x,70*members.Length);
                for(int i = 0;i < members.Length;i++){
                    GameObject UserScore = Instantiate(prefab,scrollViewContent);
                    if(i == 0){
                        UserScore.GetComponent<Image>().color = new Color(255,215,0,255);
                    } else if(i == 1){
                        Debug.Log("1");
                        UserScore.GetComponent<Image>().color = new Color(192,192,192,255);
                    } else if(i == 2){
                        Debug.Log("2");
                        UserScore.GetComponent<Image>().color = new Color(80,50,20,255);
                    } else {
                        Debug.Log("Resto");
                        UserScore.GetComponent<Image>().color = new Color(255,255,255,255);
                    }
                    this.dynamicScore.Add(UserScore);
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

    public IEnumerator showLeaderBoard(){
        yield return this.FetchTopHighScoresRoutine();
    }

    
    public void PosButtonEvent(){

    }

    public void UserButtonEvent(){

    }

    public void TimeButtonEvent(){

    }
}
