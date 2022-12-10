using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void ChangeScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID,LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void NextLevel(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
    void Start(){
        if(SceneManager.GetActiveScene().buildIndex > 3){
            BGMusic.instance.GetComponent<AudioSource>().Stop();
        }
    }
}
