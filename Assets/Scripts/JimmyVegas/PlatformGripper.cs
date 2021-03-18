using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
  // void OnCollisionStay(Collision other) {
  //     print("DA");
  //     if (other.gameObject.tag != "Player") return;
  //     if (other.transform.parent != this.transform.parent) {
  //         other.transform.SetParent(this.transform.parent);
  //     }
  // }

  // void OnCollisionExit() {
  //     GameObject.FindGameObjectWithTag("Player").transform.parent = GameObject.Find("##### PLAYER #####").transform;
  // }

  void OnTriggerStay(Collider other)
  {
    if (other.gameObject.tag != "Player") return;
    if (other.transform.parent != this.transform.parent)
    {
      other.transform.SetParent(this.transform.parent);
    }
  }

  void OnTriggerExit()
  {
    GameObject.FindGameObjectWithTag("Player").transform.parent = GameObject.Find("##### PLAYER #####").transform;
  }
}
