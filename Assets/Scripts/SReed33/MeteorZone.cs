// Sreed33
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorZone : MonoBehaviour
{
  [Header("Meteor Zone Settings")]

  [Tooltip("The tag assigned to the GameObject that spawn meteors")]
  [SerializeField] private string objectTag;

  [Tooltip("Time between meteor spawns")]
  [SerializeField] private float spawnInterval;


  [Header("Meteor Prefab Assets")]

  [Tooltip("Small meteor prefab")]
  [SerializeField] private GameObject meteorSmall;

  [Tooltip("Medium meteor prefab")]
  [SerializeField] private GameObject meteorMedium;

  [Tooltip("Large meteor prefab")]
  [SerializeField] private GameObject meteorLarge;


  [Header("Zone State")]

  [Tooltip("Automatically initialized with spawn positions in this zone on game start")]
  [SerializeField] private List<GameObject> meteorSpawners;

  [Tooltip("A local bool that controls this zone spawning meteors")]
  [SerializeField] private bool zoneSpawnMeteors = false;

  [Tooltip("A local bool used to throttle the spawn rate of meteors")]
  [SerializeField] private bool spawnPending = false;

  // A static bool used to control the spawning of meteors across the entire level
  public static bool spawnMeteors = true;


  /**********************************************************************************************/
  // Unity / game loop functions

  // Start is called before the first frame update
  void Start()
  {
    spawnMeteors = true;
    // Check each object that makes up this meteor zone
    foreach (Transform child in this.transform.parent.GetComponentsInChildren<Transform>())
    {
      // If the object is tagged as a metor spawner, add it to the list
      if (child.gameObject.tag == objectTag) meteorSpawners.Add(child.gameObject);
    }
  }

  void FixedUpdate()
  {
    // Do nothing if either local zone or level meteor bool is false 
    if (!spawnMeteors || !zoneSpawnMeteors) return;
    // StartCoroutine only after the previous meteor is spawned
    if (!spawnPending) StartCoroutine(SpawnMeteor());
  }

  void OnTriggerEnter(Collider other)
  {
    // Do nothing if the object is not a player
    if (other.gameObject.tag != "Player") return;
    // Set local meteor spawn bool to true; Start spawning meteors in this zone
    zoneSpawnMeteors = true;
  }

  void OnTriggerStay(Collider other)
  {
    if (other.gameObject.tag != "Player") return;
    // If there is a player in the zone and we are not spawning meteors, set zoneSpawnMeteors
    if (!zoneSpawnMeteors) zoneSpawnMeteors = true;
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag != "Player") return;
    // When the player leaves the zone, stop spawning meteors
    zoneSpawnMeteors = false;
  }


  /**********************************************************************************************/
  // Implementation

  IEnumerator SpawnMeteor()
  {
    spawnPending = true;
    // Wait for the set number of seconds until we spawn a meteor
    yield return new WaitForSeconds(spawnInterval);

    // Using the count of objects in the meteorSpawner list, get a random index to spawn at
    int spawnSlot = Random.Range(0, meteorSpawners.Count);
    Transform spawnPos = meteorSpawners[spawnSlot].transform;

    // Using 10 values (0-9), get a large meteor(20%), medium meteor(30%), or small meteor(50%)
    // + large = 0, 1; Medium = 4, 3, 2; Small = 5, 6, 7, 8, 9 
    int meteorType = Random.Range(0, 9);
    GameObject newMeteor;
    if (meteorType < 2) newMeteor = meteorLarge;
    else if (meteorType < 5 && meteorType >= 2) newMeteor = meteorMedium;
    else newMeteor = meteorSmall;

    // Spawn this meteor
    GameObject.Instantiate(newMeteor, spawnPos.position, meteorSpawners[0].transform.rotation);
    // Spawn another meteor in FixedUpdate()
    spawnPending = false;
  }
}
