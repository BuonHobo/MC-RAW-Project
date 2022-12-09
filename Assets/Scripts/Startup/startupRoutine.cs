using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class startupRoutine : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject screenBlock;
    [SerializeField] TextMeshProUGUI userNameText;

    private string userName;
    void Start(){
        this.screenBlock.SetActive(true);                                       // enables "loading screen"
        this.SetupRoutine();
        this.screenBlock.SetActive(false);                                      // disables "loading screen
    }

    /*======================================================================================================================*/
    /* STARTUP SETUP*/

    void SetupRoutine(){
        BGMusic.instance.GetComponent<AudioSource>().Play();
        this.userName = PlayerPrefs.GetString("UserName","Guest");
        this.initializePlayer();
        this.initializeLootLockerSession();

        // set name of current player
        this.userNameText.SetText(this.userName);
    }

    void initializePlayer(){
        PlayerPrefs.SetString("UserName",this.userName);
    }

    void initializeLootLockerSession(){
        LootLockerSDKManager.StartSession(this.userName,(response) => {
            LootLockerSDKManager.SetPlayerName(this.userName,(response) => {});
            if(response.success) Debug.Log("Logged in");
        });
    }

    /*======================================================================================================================*/

}
