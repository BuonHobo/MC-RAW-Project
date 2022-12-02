using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class FinishScript : MonoBehaviour
{
    [SerializeField] ItemCollector item;
    [SerializeField] GameObject WinMenu;
    [SerializeField] Chronometer chronometer;

    public void Start(){

    }

    public int leaderBoardID(){
        if(SceneManager.GetActiveScene().buildIndex == 4) return 9291;
        else if(SceneManager.GetActiveScene().buildIndex == 5) return 9292;
        else if(SceneManager.GetActiveScene().buildIndex == 6) return 9293;
        else return 9290;
    }

    public void SubmitScoreRoutine(int scoreToUpload){
        string playerID = PlayerPrefs.GetString("PlayerID");
        int lB_ID = this.leaderBoardID();
        LootLockerSDKManager.SubmitScore(playerID,scoreToUpload,lB_ID,(response) =>
        {
            if(response.success){
                Debug.Log("Succesfully uploaded score");
            } else {
                Debug.Log("Failed" + response.Error);
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerCharacter" && item.collected){
            Invoke("CompleteLevel",0.5f);
        }
    }

    public void CompleteLevel()
    {
        Time.timeScale = 0f;
        WinMenu.SetActive(true);
        SubmitScoreRoutine(((int) (chronometer.time * 1000)));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID,LoadSceneMode.Single);
    }
}

