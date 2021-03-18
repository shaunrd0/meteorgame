//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
  public class ThirdPersonCamera : MonoBehaviour
  {
    public float smooth = 3f;   // カメラモーションのスムーズ化用変数
    Transform standardPos;      // the usual position for the camera, specified by a transform in the game
    Transform frontPos;     // Front Camera locater
    Transform jumpPos;      // Jump Camera locater


    // Sreed333
    // Added this for additional lookAt position when holding RMB
    Transform lookAt; // lookAt position


    // Sreed33
    // Removed this from logic to simplify code
    // bool bQuickSwitch = false;	//Change Camera Position Quickly


    void Start()
    {
      // 各参照の初期化
      standardPos = GameObject.Find("CamPos").transform;

      if (GameObject.Find("FrontPos"))
        frontPos = GameObject.Find("FrontPos").transform;

      if (GameObject.Find("JumpPos"))
        jumpPos = GameObject.Find("JumpPos").transform;


      // Sreed33
      // Added a position for a top-down view on the player
      if (GameObject.Find("LookAtPos"))
        lookAt = GameObject.Find("LookAtPos").transform;


      //カメラをスタートする
      transform.position = standardPos.position;
      transform.forward = standardPos.forward;
    }

    void FixedUpdate()  // このカメラ切り替えはFixedUpdate()内でないと正常に動かない
    {
      // Sreed33
      // Make sure player controls are enabled
      if (!UnityChan.UnityChanControlScriptWithRgidBody.controlsActive) return;

      if (Input.GetButton("Fire1"))
      { // left Ctlr	
        // Change Front Camera
        setCameraPositionFrontView();
      }
      else if (Input.GetButton("Fire2"))
      { //Alt	


        // Sreed33
        // Call my function instead of setPositionJumpView
        //setCameraPositionJumpView();
        setLookAtView();
      }
      else
      {
        // return the camera to standard position and direction
        setCameraPositionNormalView();
      }
    }

    void setCameraPositionNormalView()
    {
      // if (bQuickSwitch == false) {
      // the camera to standard position and direction
      // print("False")
      transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);


      // Sreed33
      // Replace commented Vector3.Lerp with Quaternion.Lerp to rotate camera correctly when moving through portals
      // + I did something similar in all the setCameraPosition functions
      transform.rotation = Quaternion.Lerp(transform.rotation, standardPos.rotation, Time.fixedDeltaTime * smooth);
      // transform.forward = Vector3.Lerp (transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);



      // } else {
      // the camera to standard position and direction / Quick Change
      // transform.position = standardPos.position;	
      // transform.forward = standardPos.forward;
      // bQuickSwitch = false;
      // }
    }

    void setCameraPositionFrontView()
    {
      // Change Front Camera
      // bQuickSwitch = true;


      // Sreed33
      // Replace commented frontPos.forward with Quaternion.Lerp to rotate camera according to gravity direction
      transform.position = Vector3.Lerp(transform.position, frontPos.position, Time.fixedDeltaTime * smooth);
      // transform.position = frontPos.position;	
      transform.rotation = Quaternion.Lerp(transform.rotation, frontPos.rotation, Time.fixedDeltaTime * smooth);
      // transform.forward = frontPos.forward;
    }


    // Sreed33
    // This function is no longer in use anywhere
    void setCameraPositionJumpView()
    {
      // Change Jump Camera
      // bQuickSwitch = false;
      transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);
      transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);
    }


    // Sreed33
    // Added function to change to top-down camera view
    // Makes it easier to avoid meteors when there is a lot of them
    void setLookAtView()
    {
      transform.position = Vector3.Lerp(transform.position, lookAt.position, Time.fixedDeltaTime * smooth);
      transform.rotation = Quaternion.Lerp(transform.rotation, lookAt.rotation, Time.fixedDeltaTime * smooth);
    }

  }
}