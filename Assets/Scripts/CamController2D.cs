using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController2D : MonoBehaviour {

	GameObject player;
	Camera cam;
	public Vector2 focusAreaFollowOffset;
	public Vector2 focusAreaSize;
	public float smoothX;
	public float smoothY;
	struct FocusArea{
		public Vector2 center;
		float left,right,bottom,top;
		public FocusArea(Bounds targetBounds, Vector2 size){
			left = targetBounds.center.x-size.x/2;
			right = targetBounds.center.x+size.x/2;
			bottom = targetBounds.min.y;
			top = targetBounds.min.y + size.y;
			center = new Vector2(targetBounds.center.x,targetBounds.min.y+size.y/2);
		}
		public void FollowBounds(Bounds targetBounds){
			float shiftX = 0;
			float shiftY = 0;
			if ((targetBounds.center.x - targetBounds.extents.x) < left) {
				shiftX = targetBounds.center.x - targetBounds.extents.x - left;
			}else if((targetBounds.center.x + targetBounds.extents.x) > right){
				shiftX = targetBounds.center.x + targetBounds.extents.x - right;
			}

			if ((targetBounds.center.y - targetBounds.extents.y) < bottom) {
				shiftY = targetBounds.center.y - targetBounds.extents.y - bottom;
			}else if((targetBounds.center.y + targetBounds.extents.y) > top){
				shiftY = targetBounds.center.y + targetBounds.extents.y - top;
			}
			center.x += shiftX;
			left += shiftX;
			right += shiftX;
			center.y += shiftY;
			bottom += shiftY;
			top += shiftY;
		}
	}

	FocusArea focusArea;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		cam = GetComponent<Camera> ();
		focusArea = new FocusArea (player.GetComponent<Collider>().bounds,focusAreaSize);
	}
	// Update is called once per frame
	void FixedUpdate(){
		CameraFollow ();
	}
	void CameraFollow(){
		focusArea.FollowBounds (player.GetComponent<Collider>().bounds);
		float posX = Mathf.Lerp(transform.position.x,focusArea.center.x + focusAreaFollowOffset.x,smoothX);
		float posY = Mathf.Lerp(transform.position.y,focusArea.center.y + focusAreaFollowOffset.y,smoothY);
		transform.position = new Vector3 (posX,posY,transform.position.z) ;
	}
	void OnDrawGizmos(){
		Color c = Color.red;
		c.a = 0.3f;
		Gizmos.color = c;
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}
}
