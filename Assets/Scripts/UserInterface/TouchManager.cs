using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
public class TouchManager : Singleton<TouchManager> {

	// Use this for initialization

	private GameObject[] touchesOld = new GameObject[1];
	private List<GameObject> touchList = new List<GameObject>();
	private Camera camera;
	private RaycastHit hit;
	public LayerMask touchReceivingLayer;
	void Start(){
		camera = Camera.main;
	}

	public Ray PrimaryRay;
	void FixedUpdate () {
		bool isHit;
		if (Input.touches.Length > 0) {
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();
			foreach (Touch touch in Input.touches) {
				PrimaryRay = camera.ScreenPointToRay (touch.position);
				isHit = Physics.Raycast (PrimaryRay, out hit, 1000f, touchReceivingLayer);
				//Debug.Log (isHit);
				if(isHit){
					GameObject recipient = hit.transform.gameObject;

					if (touch.phase == TouchPhase.Began) {

						touchList.Add (hit.transform.gameObject);
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Stationary) {
						touchList.Add (hit.transform.gameObject);
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Moved) {
						touchList.Add (hit.transform.gameObject);
						foreach (GameObject g in touchesOld) {
							if (!touchList.Contains (g) && g != null) {
								g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
							}
						}
						recipient.SendMessage ("OnTouchMoved", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Canceled) {
						recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		foreach (GameObject g in touchesOld) {
			if (!touchList.Contains (g) && g != null) {
				g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (PrimaryRay.origin,PrimaryRay.origin+PrimaryRay.direction*10000f);
	}
}

