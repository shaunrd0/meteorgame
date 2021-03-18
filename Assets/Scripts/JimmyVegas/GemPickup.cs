using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPickup : MonoBehaviour
{
  public GameObject scoreBox;
  public AudioSource collectSound;
  public int gemValue;
  public int rotateSpeed = 2;

  void Update()
  {
    // Sreed33
    // Had to change this to Space.Self to rotate gems correctly on level2
    // + Gems on walls an ceilngs were spinning on wrong axis
    transform.Rotate(0, rotateSpeed * Time.timeScale, 0, Space.Self);
  }

  void OnTriggerEnter(Collider other)
  {
    // If the object is anything  other than a player, do nothing
    if (!other.CompareTag("Player")) return;
    GlobalScore.currentScore += gemValue;
    collectSound.Play();
    Destroy(gameObject);
  }

}
