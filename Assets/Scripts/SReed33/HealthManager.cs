// SReed33
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
  [Header("Health Settings")]

  [Tooltip("The amount to take from the player's score when being hit by a meteor")]
  [SerializeField] private int scorePenalty = 500;

  [Tooltip("The length of time for the player to be invincible after being hit")]
  [SerializeField] private float invTime = 2.0f;

  [Tooltip("The icon to show when the player is invincible")]
  [SerializeField] private GameObject sheildIcon;

  [Tooltip("The sphere that acts as a forcefield around the player when invincible")]
  [SerializeField] private GameObject forceField;

  [Tooltip("The HeartPanel to access player hearts")]
  [SerializeField] private GameObject heartPanel;

  [Tooltip("The YouDied UI panel to enable when the player dies")]
  [SerializeField] private GameObject youDied;

  [Tooltip("The FadeOut UI panel to enable when the player dies")]
  [SerializeField] private GameObject fadeOut;


  [Header("Health State")]

  [Tooltip("Player hearts, automatically adjusts to UI elements")]
  [SerializeField] private GameObject[] playerHearts;

  [Tooltip("Player hits, initialized at game start based on heart count with playerHearts.Length")]
  [SerializeField] private int playerHits;

  [Tooltip("Currrent invincible state of the player")]
  [SerializeField] private bool playerInvincible = false;


  /**********************************************************************************************/
  // Unity / game loop functions

  // Start is called before the first frame update
  void Start()
  {
    playerHits = heartPanel.transform.childCount;
  }


  /**********************************************************************************************/
  // Implementation

  // Called by Meteor.cs when a meteor hits the player
  public void HurtPlayer()
  {
    // Do nothing if we are invincible; Don't try to reach before child index 0, it doesnt exist
    if (playerInvincible) return;
    if (playerHits < 0) return;

    // Reduce the score of the player for being hit
    GlobalScore.currentScore -= scorePenalty;

    // Get the last child of the heart's UI panel and deactivate it
    // Hides a heart from view on each hit
    heartPanel.transform.GetChild(--playerHits).gameObject.SetActive(false);

    // If we just removed the last heart, we died :(
    if (playerHits == 0) StartCoroutine(KillPlayer());
    else StartCoroutine(Invincible()); // Otherwise, make player invincible for invTime
  }

  // Makes the player invincible for the time set in the editor
  IEnumerator Invincible()
  {
    // Set the invincibility UI element to active
    forceField.SetActive(true);
    sheildIcon.SetActive(true);
    playerInvincible = true;
    yield return new WaitForSeconds(invTime);

    // Deactivate invincibility after waiting is done
    forceField.SetActive(false);
    sheildIcon.SetActive(false);
    playerInvincible = false;
  }

  // Ends the game when the player loses all their health
  IEnumerator KillPlayer()
  {
    // Disable player controls and level BGM
    UnityChan.UnityChanControlScriptWithRgidBody.controlsActive = false;
    GameObject.Find("LevelAudio").GetComponent<AudioSource>().Pause();

    // Activate youDied, fadeOut overlays and load next scene
    youDied.SetActive(true);
    yield return new WaitForSeconds(2);
    fadeOut.SetActive(true);
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(Level.thisLevel);
  }

}
