using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePanelController : MonoBehaviour {

	public GameObject PointIndicator;
	Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray camToTouchRay = new Ray(cam.transform.position,ActionTouchReiceiver.Instance.PrimaryPoint);
		Plane plane = new Plane ();

	}

}
