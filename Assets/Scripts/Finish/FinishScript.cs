using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    [SerializeField] ItemCollector item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PlayerCharacter" && item.collected){
            Invoke("CompleteLevel",0.5f);
        }
    }

    private void CompleteLevel(){
         if(SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings-1))
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         }
         else
         {
            SceneManager.LoadScene(0);
         }
    }
}

