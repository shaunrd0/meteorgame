using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDeath : MonoBehaviour
{
  public GameObject youFell;
  public GameObject levelAudio;
  public GameObject fadeOut;

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag != "Player") return;
    StartCoroutine(YouFellOff());
  }

  IEnumerator YouFellOff()
  {
    youFell.SetActive(true);
    levelAudio.SetActive(false);
    yield return new WaitForSeconds(2);
    fadeOut.SetActive(true);
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(Level.thisLevel);
  }
}
