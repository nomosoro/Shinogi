using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveRightButton : MonoBehaviour {
	// Use this for initialization
	SpriteRenderer sr;
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnTouchMoved(){
		UnfadeButton ();
		PlayerInput.Instance.MoveHorizontal = 1;
	}
	void OnTouchStay(){
		UnfadeButton ();
		PlayerInput.Instance.MoveHorizontal = 1;
	}
	void OnTouchDown(){
		UnfadeButton ();
		PlayerInput.Instance.MoveHorizontal = 1;
	}
	void OnTouchExit(){
		FadeButton ();
		PlayerInput.Instance.MoveHorizontal = 0;
	}
	void OnTouchUp(){
		FadeButton ();
		PlayerInput.Instance.MoveHorizontal = 0;
	}
	void FadeButton(){
		Color c = sr.color;
		c.a = 0.5f;
		sr.color = c;
	}
	void UnfadeButton(){
		Color c = sr.color;
		c.a = 1;
		sr.color = c;
	}
}
