using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePanelController : MonoBehaviour {

	public GameObject PointIndicator;
	Camera cam;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}

	void Update () {
		Plane plane = new Plane (transform.forward,transform.position);
		float enterDis;
		if (plane.Raycast (TouchManager.Instance.PrimaryRay, out enterDis)) {
			Vector3 worldPoint = TouchManager.Instance.PrimaryRay.GetPoint (enterDis);
			PointIndicator.transform.position = worldPoint;
		}
		Debug.Log (ActionTouchReiceiver.Instance.WorldPoint);
	}

	void OnGizmosDraw(){
		Gizmos.color = Color.red;
	}
	
}
