using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
  [Tooltip("The fadeIn gameobject to activate when the level starts")]
  [SerializeField] public GameObject fadeIn;

  [Tooltip("The name of the next level to load")]
  [SerializeField] private string _nextLevel;

  [Tooltip("The name of this level")]
  [SerializeField] private string _thisLevel;

  // Static values used to control game sequence
  public static string nextLevel;
  public static string thisLevel;


  void Start()
  {
    nextLevel = _nextLevel;
    thisLevel = _thisLevel;
    GlobalScore.currentScore = 0;
    // Reset gravity to default on level start
    Physics.gravity = Vector3.down * 9.81f;
    StartCoroutine(FadeIn());
  }

  IEnumerator FadeIn()
  {
    fadeIn.SetActive(true);
    yield return new WaitForSeconds(1);
    fadeIn.SetActive(false);
  }

}