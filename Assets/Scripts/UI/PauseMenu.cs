using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject Music;
    public void Pause()
    {
        this.Music.GetComponent<AudioSource>().Pause();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    public void Resume()
    {
        this.Music.GetComponent<AudioSource>().UnPause();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
    }
}
