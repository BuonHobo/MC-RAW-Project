using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Startup : MonoBehaviour {
 
    public static GameObject PauseMenuPanel;
 
    void Start ()
    {
        PauseMenuPanel = GameObject.Find("PauseMenu");
        PauseMenuPanel.SetActive(false);
    }
 
 }

public class PauseButton : MonoBehaviour
{
    //private static GameObject PauseMenuPanel;
    //private bool paused = false;
    // Start is called before the first frame 
    /*
    void Start()
    {
        //PauseMenuPanel = GameObject.Find("PauseMenu");
        //PauseMenuPanel.SetActive(false);
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") || CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            //Time.timeScale = 0;
            Debug.Log(Startup.PauseMenuPanel);

            /*
            if (!paused)
            {
                Time.timeScale = 0;
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                paused = false;
            }
            */
        }
    }
}
