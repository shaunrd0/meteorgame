using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour
{
  public GameObject levelMusic;
  public AudioSource levelComplete;
  public GameObject levelTimer;
  public GameObject timeLeft;
  public GameObject theScore;
  public GameObject totalScore;
  public GameObject fadeOut;
  public int timeCalc;
  public int scoreCalc;
  public int totalScored;

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag != "Player") return;
    GetComponent<BoxCollider>().enabled = false;
    levelMusic.SetActive(false);
    levelTimer.SetActive(false);
    levelComplete.Play();
    StartCoroutine(CalculateScore());
  }

  IEnumerator CalculateScore()
  {
    // Stop the spawning of meteors across the whole level
    MeteorZone.spawnMeteors = false;

    // Reward the player with extra points for the time left on the clock
    timeCalc = GlobalTimer.extendScore * 100;
    totalScored = GlobalScore.currentScore + timeCalc;

    // SReed33 
    // Changed high score system slightly to show best score for entire playthroughs 
    // Store this level score in playerprefs
    PlayerPrefs.SetInt("LevelScore", totalScored);
    // Store the total for this playthrough in playerprefs
    PlayerPrefs.SetInt("RunningTotal", PlayerPrefs.GetInt("RunningTotal") + totalScored);
    // Check if we beat the high score  with this playthrough
    print("RunningTotal: " + PlayerPrefs.GetInt("RunningTotal"));
    if (PlayerPrefs.GetInt("BestScore") < PlayerPrefs.GetInt("RunningTotal"))
    {
      PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("RunningTotal"));
    }

    // Show the bonus applied for time remaining
    timeLeft.SetActive(true);
    SetChildText(timeLeft, "Time Left: " + GlobalTimer.extendScore + " x100");
    yield return new WaitForSeconds(1);

    // Give UnityChan 1 second to settle position, then freeze input
    // + Also rotates camera to frontView for victory screen
    UnityChan.UnityChanControlScriptWithRgidBody.controlsActive = false;

    // Show the score from collectables gathered on this level
    theScore.SetActive(true);
    SetChildText(theScore, "Score: " + GlobalScore.currentScore);
    yield return new WaitForSeconds(1);

    // Show the calculated total score
    totalScore.SetActive(true);
    SetChildText(totalScore, "Total Score: " + totalScored);
    yield return new WaitForSeconds(3);

    // Activate the fadeOut gameobject to fade to next level
    fadeOut.SetActive(true);
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene(Level.nextLevel);
  }

  // Helper function to set the text of multiple fields that share a parent
  void SetChildText(GameObject parent, string text)
  {
    foreach (Text t in parent.GetComponentsInChildren<Text>())
    {
      t.text = text;
    }
  }

}
