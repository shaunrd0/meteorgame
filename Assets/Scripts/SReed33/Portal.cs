// Sreed33
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
  [Tooltip("The linked portal to move the player to when entering here")]
  [SerializeField] private GameObject friendPortal;

  // Boolean to prevent teleporting back and forth infinitely
  public static bool teleporting = false;


  /**********************************************************************************************/
  // Unity / game loop functions

  void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag != "Player") return;
    // Only teleport if we are not teleporting
    if (!teleporting) TeleportChan(other);
  }


  /**********************************************************************************************/
  // Implementation

  void TeleportChan(Collision player)
  {
    teleporting = true;
    // Stop applying physics to the player until we finish teleporting
    player.rigidbody.isKinematic = true;

    // Move the player to the spawn position that is always the only child of Portal.cs
    Transform spawnPos = friendPortal.GetComponentInChildren<Portal>().transform.GetChild(0);
    player.transform.position = spawnPos.position;
    player.transform.rotation = spawnPos.rotation;

    // Always force the player down towards whatever surface they are waling on
    // + Allows player to walk on walls and ceilings after TP using local->world axis
    Physics.gravity = spawnPos.up * -9.81f;
    // Resume physics
    player.rigidbody.isKinematic = false;
    teleporting = false;
  }

}
