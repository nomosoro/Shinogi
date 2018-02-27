using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveLeftButton : MonoBehaviour {
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
		SetPlayerMoveHorizontal (-1);
	}
	void OnTouchStay(){
		UnfadeButton ();
		SetPlayerMoveHorizontal (-1);
	}
	void OnTouchDown(){
		UnfadeButton ();
		SetPlayerMoveHorizontal (-1);
	}
	void OnTouchExit(){
		FadeButton ();
		SetPlayerMoveHorizontal (0);
	}
	void OnTouchUp(){
		FadeButton ();
		SetPlayerMoveHorizontal (0);
	}

	public void SetPlayerMoveHorizontal(int value){
		PlayerInput.Instance.MoveHorizontal = value;
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
