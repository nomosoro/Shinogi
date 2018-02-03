using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerWorldCanvas : MonoBehaviour {

	public GameObject followedGameObject;
	private Vector3 distance;
	// Use this for initialization
	void Start () {
		distance = transform.position - followedGameObject.transform.position ;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = followedGameObject.transform.position + distance;
	}
}
