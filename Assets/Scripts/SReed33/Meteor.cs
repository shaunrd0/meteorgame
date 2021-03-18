// Sreed33
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
  [Header("Meteor Settings")]

  [Tooltip("The root gameobject to enable for playing explosion effects")]
  [SerializeField] private GameObject explosionEffect;

  [Tooltip("The radius of the explosion force created on impact")]
  [SerializeField] private float explosionRadius;

  [Tooltip("The force of the explosion")]
  [SerializeField] private float explosionForce;

  [Tooltip("The upwards lift from the explosion")]
  [SerializeField] private float explosionLift;

  [Tooltip("The radius used to damage the player")]
  [SerializeField] private float damageRadius;

  [Tooltip("The radius used to destroy blocks")]
  [SerializeField] private float destroyRadius;

  [Tooltip("The maximum number of blocks to destroy")]
  [SerializeField] private int destroyBlocksCount;

  [Tooltip("Set true if the meteor should destroy blocks on collision")]
  // Large meteors are set in the editor to destroy blocks
  [SerializeField] private bool destroyBlocks = false;


  [Header("Meteor State")]

  [Tooltip("Once this boolean is false, the meteor will not trigger events again")]
  [SerializeField] private bool newMeteor = true;


  /**********************************************************************************************/
  // Unity / game loop functions

  void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<GemPickup>()) return;
    DamagePlayer();
    // Track if this is the first collision for this meteor
    if (newMeteor) newMeteor = false;
    else return;
    // If it has already collided, then do nothing

    if (destroyBlocks) DestroyBlocks();

    // Disable meteor components, enable explosion effect
    GetComponent<MeshRenderer>().enabled = false;
    GetComponent<SphereCollider>().enabled = false;
    foreach (Light l in GetComponentsInChildren<Light>()) l.enabled = false;
    // I attached box collider to explosion effects so they would collide with the ground
    // Use it to enable the root gameobject and play all the nested effects
    explosionEffect.SetActive(true);

    // Check all colliders within a radius around the explosion
    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (Collider c in colliders)
    {
      // If it is not a player, do nothing; Otherwise hurt the player
      if (c.gameObject.tag != "Player") continue;

      // Apply a force to the player from the explosion
      Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
      if (rb != null)
      {
        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionLift, ForceMode.Impulse);
      }
    }

    // Destroy the meteor
    StartCoroutine(KillMeteor());
  }

  /**********************************************************************************************/
  // Implementation

  void DestroyBlocks()
  {
    int blocksDestroyed = 0;
    Collider[] blocks = Physics.OverlapSphere(transform.position, destroyRadius);
    foreach (Collider c in blocks)
    {
      if (c.gameObject.tag == "Blue")
      {
        Destroy(c.gameObject);
        blocksDestroyed++;
        if (blocksDestroyed >= destroyBlocksCount) return;
      }
    }
  }

  void DamagePlayer()
  {
    Collider[] blocks = Physics.OverlapSphere(transform.position, damageRadius);
    foreach (Collider c in blocks)
    {
      if (c.gameObject.tag == "Player")
      {
        print("Ow: " + this.gameObject.name);
        FindObjectOfType<HealthManager>().HurtPlayer();
      }
    }
  }

  IEnumerator KillMeteor()
  {
    // Wait two seconds before destroying this gameobject to let the explosion finish
    yield return new WaitForSeconds(2.0f);
    Destroy(this.gameObject);
  }
}
