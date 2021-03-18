using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
  [Header("Load Screen Settings")]

  [Tooltip("Object(s) to activate when the scene starts")]
  [SerializeField] private GameObject[] someObjects;

  [Tooltip("The amount of time to wait before loading the next level")]
  [SerializeField] private float waitTime;

  [Tooltip("The name of the next level to load")]
  [SerializeField] private string levelToLoad;


  /**********************************************************************************************/
  // Unity / game loop functions

  void Start()
  {
    StartCoroutine(Transition());
  }


  /**********************************************************************************************/
  // Implementation

  IEnumerator Transition()
  {
    yield return new WaitForSeconds(0.5f);

    // Activate the objeccts set in the inspector, then waitTime
    foreach (GameObject o in someObjects) o.SetActive(true);
    yield return new WaitForSeconds(waitTime);

    // Load the next levelToLoad
    SceneManager.LoadScene(levelToLoad);
  }

}
