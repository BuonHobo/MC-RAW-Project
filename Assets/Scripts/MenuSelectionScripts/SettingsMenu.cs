using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNameInputField;

    public void setPlayerName(){
        PlayerPrefs.SetString("PlayerName",playerNameInputField.text);
    }
}
