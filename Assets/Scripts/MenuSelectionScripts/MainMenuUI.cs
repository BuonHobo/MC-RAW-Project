using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;

    void Start(){
        this.Authentication(PlayerPrefs.GetString("PlayerName","Guest"));
        userNameText.SetText(PlayerPrefs.GetString("PlayerName","Guest"));
    }

    public void Authentication(string userName){
        LootLockerSDKManager.StartSession(userName,(response) => {
            LootLockerSDKManager.SetPlayerName(userName,(response) => {});
            if(response.success) Debug.Log("Logged in");
        });
    }
}
