using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;
using UnityEngine.UI;

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
    private int maxLevel;
    private int minLevel;
    void Start()
    {
        this.dynamicScore = new List<GameObject>();
        this.minLevel = 1;
        this.currentLevel = 1;
        this.maxLevel = 4;
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
        LootLockerSDKManager.GetScoreList(this.getLeaderBoardID(),10,0,(response) => {
            foreach(GameObject score in this.dynamicScore) Destroy(score);
            if(response.success){
                LootLockerLeaderboardMember[] members = response.items;
                for(int i = 0;i < members.Length;i++){
                    GameObject UserScore = Instantiate(prefab,scrollViewContent);
                    this.dynamicScore.Add(UserScore);
                    TextMeshProUGUI[] infos = UserScore.GetComponentsInChildren<TextMeshProUGUI>();
                    infos[0].SetText((i + 1).ToString());
                    infos[1].SetText(members[i].player.name);
                    infos[2].SetText((members[i].score).ToString());
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
