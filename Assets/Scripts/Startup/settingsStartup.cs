using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class settingsStartup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PlaceHolder;
    [SerializeField] TMP_InputField input;
    void Start(){
        this.PlaceHolder.SetText(PlayerPrefs.GetString("UserName","Guest"));
    }

    public void setPlayerName(){
        this.input.text = this.input.text.Replace("\u200b","").Trim();
        Debug.Log(input.text.Length);
        if(this.input.text.Length == 0){
            return;
        } else {
            PlayerPrefs.SetString("UserName",input.text);
            Debug.Log("Modified Username: " + PlayerPrefs.GetString("UserName","Guest"));
            Debug.Log("input: " + this.input.text);
        }
        
    }
}
