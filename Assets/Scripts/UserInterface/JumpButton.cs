using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumpButton : MonoBehaviour {
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
		PlayerInput.Instance.Jump = false;
	}
	void OnTouchStay(){
		UnfadeButton ();
		PlayerInput.Instance.Jump = false;
	}
	void OnTouchDown(){
		UnfadeButton ();
		PlayerInput.Instance.Jump = true;
	}
	void OnTouchExit(){
		FadeButton ();
		PlayerInput.Instance.Jump = false;
	}
	void OnTouchUp(){
		FadeButton ();
		PlayerInput.Instance.Jump = false;
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
