using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI LevelIndicator;
    [SerializeField] GameObject PreviousLeaderBoardButton;
    [SerializeField] GameObject NextLeaderBoardButton;

    [SerializeField] GameObject PosButton;
    [SerializeField] GameObject UserButton;
    [SerializeField] GameObject TimeButton;
    [SerializeField] GameObject DateButton;
    private int currentLevel;
    private int maxLevel;
    private int minLevel;
    void Start()
    {
        this.minLevel = 1;
        this.currentLevel = 1;
        this.maxLevel = 5;
        checkLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviousLeaderBoard(){
        this.currentLevel -= 1;
        this.LevelIndicator.SetText(this.currentLevel.ToString());
        this.checkLevel();
    }

    public void NextLeaderBoard(){
        this.currentLevel += 1;
        this.LevelIndicator.SetText(this.currentLevel.ToString());
        this.checkLevel();
    }

    void checkLevel(){
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

    public void showLeaderBoard(){

    }

    public void PosButtonEvent(){

    }

    public void UserButtonEvent(){

    }

    public void TimeButtonEvent(){

    }

    public void DateButtonEvent(){

    }
}
