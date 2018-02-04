using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class PlayerInput : Singleton<PlayerInput> {
	public KeyCode moveForwardKey;
	public KeyCode moveBackwardKey;
	public KeyCode moveLeftKey;
	public KeyCode moveRightKey;
	public KeyCode jumpKey;
	public KeyCode shootKey;


	public float MoveVertical{ get;set;}
	public float MoveHorizontal{ get; set;}
	public bool Jump{ get; set;}
	public bool Shoot{ get; set; }
	public bool clickDown{ get; set;}
	public Vector3 clickPos{ get; set;}
	public bool usingTouchscreen = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (!usingTouchscreen) {
			DetectKeyboardNMouse ();
		}
	}
	void DetectKeyboardNMouse(){
		MoveVertical = 0;
		if (Input.GetKey (moveForwardKey)) {
			MoveVertical += 1;
		}
		if (Input.GetKey (moveBackwardKey)) {
			MoveVertical -= 1;
		}
		MoveHorizontal = 0;
		if (Input.GetKey (moveRightKey)) {
			MoveHorizontal += 1;
		}
		if (Input.GetKey (moveLeftKey)) {
			MoveHorizontal -= 1;
		}
		Jump = Input.GetKeyDown (jumpKey);
		Shoot = Input.GetKey (shootKey);
		clickDown = Input.GetMouseButtonDown(0);
	}


}
