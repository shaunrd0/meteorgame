// Sreed33
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
  [Tooltip("Seconds to wait before destroying the block")]
  [SerializeField] private float destroyTime;


  /**********************************************************************************************/
  // Unity / game loop functions

  void OnTriggerEnter(Collider other)
  {
    if (other.tag != "Player") return;
    // When the player enters the zone, destroy the block
    StartCoroutine(DestroyBlock());
  }


  /**********************************************************************************************/
  // Implementation

  IEnumerator DestroyBlock()
  {
    float intervalCount = 5.0f;
    float interval = destroyTime / intervalCount;
    // Toggle the block mesh off->on 5 times, based on the set destroyTime
    for (int i = 0; i < intervalCount; i++)
    {
      GetComponent<MeshRenderer>().enabled = false;
      // Wait for half this iteration's normal interval
      yield return new WaitForSeconds(interval / 2.0f);
      GetComponent<MeshRenderer>().enabled = true;
      yield return new WaitForSeconds(interval / 2.0f);
    }

    Destroy(this.gameObject);
  }

}
