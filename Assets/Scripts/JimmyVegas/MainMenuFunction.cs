using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuFunction : MonoBehaviour
{
  public AudioSource buttonPress;
  public GameObject bestScoreDisplay;
  public int bestScore;

  void Start()
  {
    Cursor.visible = true;
    // Reset the running total used to track score between levels
    PlayerPrefs.SetInt("RunningTotal", 0);
    // Update the best score shown on the main menu
    bestScore = PlayerPrefs.GetInt("BestScore");
    bestScoreDisplay.GetComponent<Text>().text = "Best: " + bestScore;

    // Set the first level to be started when Play Game button is clicked
    Level.nextLevel = "Level1";
  }

  // Called by button click actions
  public void PlayGame()
  {
    buttonPress.Play();
    SceneManager.LoadScene(Level.nextLevel);
  }

  // Called by button click actions
  public void QuitGame()
  {
    buttonPress.Play();
    Application.Quit();
  }

  // Called by button click actions
  public void PlayCreds()
  {
    buttonPress.Play();
    SceneManager.LoadScene("Credits");
  }
}
