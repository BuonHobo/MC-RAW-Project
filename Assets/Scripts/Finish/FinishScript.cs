using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class FinishScript : MonoBehaviour
{
    [SerializeField] ItemCollector item;
    [SerializeField] GameObject WinMenu;
    [SerializeField] Chronometer chronometer;
    [SerializeField] TextMeshProUGUI time;

    public void Start(){
        /*
        LootLockerSDKManager.StartGuestSession((response) => {
            if(response.success){
                Debug.Log("Logged in");
            } else {
                Debug.Log("Failed to log in");
            }
        });
        */
        this.Authentication(PlayerPrefs.GetString("PlayerName","Guest"));
    }

    public void Authentication(string userName){
        LootLockerSDKManager.StartSession(userName,(response) => {
            LootLockerSDKManager.SetPlayerName(userName,(response) => {});
            if(response.success) Debug.Log("Logged in");
        });
    }

    public void SubmitScore(int score){
        LootLockerSDKManager.SubmitScore("PlayerID",score,this.leaderBoardID(),(response) => {
            if(response.success) Debug.Log("Submitted");
        });
    }

    public int leaderBoardID(){
        if(SceneManager.GetActiveScene().buildIndex == 4) return 9290;
        else if(SceneManager.GetActiveScene().buildIndex == 5) return 9291;
        else if(SceneManager.GetActiveScene().buildIndex == 6) return 9292;
        else return 9293;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerCharacter" && item.collected){
            //Invoke("CompleteLevel",0.5f);
            this.CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0f;
        WinMenu.SetActive(true);
        /*
        float record = Mathf.Round((chronometer.time * 1000));
        this.time.SetText((record/1000).ToString());
        */
        float record = chronometer.time * 1000;
        this.time.SetText((Mathf.Round(record)).ToString());
        this.SubmitScore((int) record);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}

