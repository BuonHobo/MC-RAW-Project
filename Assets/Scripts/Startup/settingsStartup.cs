using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class settingsStartup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlaceHolder;
    [SerializeField] TextMeshProUGUI playerNameInputField;
    void Start(){
        this.PlaceHolder.SetText(PlayerPrefs.GetString("UserName","Guest"));
    }

    public void setPlayerName(){
        if((int)playerNameInputField.text[0]==8203){
            return;
        } else {
            PlayerPrefs.SetString("UserName",playerNameInputField.text);
            Debug.Log("Modified Username: " + PlayerPrefs.GetString("UserName","Guest"));
            Debug.Log("input: " + this.playerNameInputField.text);
        }
    }
}
