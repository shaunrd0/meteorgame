using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
  public bool gamePaused = false;
  public AudioSource levelMusic;
  public GameObject pauseMenu;
  public AudioSource pauseJingle;
  public AudioSource buttonClickAudio;
  public Level levelScript;

  void Update()
  {
    // If we pause the game while fading in, disable fadeIn
    if (gamePaused && levelScript.fadeIn.activeSelf)
    {
      levelScript.fadeIn.SetActive(false);
    }

    // Toggle pause menu
    if (Input.GetButtonDown("Cancel"))
    {
      if (gamePaused == false)
      {
        Cursor.visible = true;
        pauseJingle.Play();
        gamePaused = true;
        levelMusic.Pause();
        pauseMenu.SetActive(true);
        // Stop game movement by setting timeScale = 0
        Time.timeScale = 0;
      }
      else
      {
        pauseMenu.SetActive(false);
        levelMusic.UnPause();
        Cursor.visible = false;
        gamePaused = false;
        // Resume game movement
        Time.timeScale = 1;
      }
    }
  }

  public void ResumeGame()
  {
    buttonClickAudio.Play();
    pauseMenu.SetActive(false);
    levelMusic.UnPause();
    Cursor.visible = false;
    gamePaused = false;
    Time.timeScale = 1;
  }

  public void RestartLevel()
  {
    buttonClickAudio.Play();
    pauseMenu.SetActive(false);
    levelMusic.UnPause();
    Cursor.visible = false;
    gamePaused = false;
    Time.timeScale = 1;
    SceneManager.LoadScene(Level.thisLevel);
  }

  public void QuitToMenu()
  {
    buttonClickAudio.Play();
    pauseMenu.SetActive(false);
    levelMusic.UnPause();
    Cursor.visible = false;
    gamePaused = false;
    Time.timeScale = 1;
    Level.nextLevel = "MainMenu";
    SceneManager.LoadScene(Level.nextLevel);
  }

}
