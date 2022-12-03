using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;
    // Start is called before the first frame update
    void Start()
    {
        string PlayerName = PlayerPrefs.GetString("PlayerName","Guest");
        this.userNameText.SetText(PlayerName);
        PlayerPrefs.SetString("PlayerName",PlayerName);
    }
}
