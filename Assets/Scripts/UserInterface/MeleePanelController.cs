using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
public class MeleePanelController : Singleton<MeleePanelController> {

	public GameObject PointIndicator;
	private Vector3 panelCenterPosition;
	private Vector3 worldPoint = Vector3.zero;
	Camera cam;	

	private Direction _pointingDirection;
	public Direction PointingDirection{ get{ return _pointingDirection;}set{ _pointingDirection = value;}}
	private float nullifiedRadius = .5f;
	private float panelRestrictionRadius = 1f;
	public Image LeftPanel;
	public Image UpPanel;
	public Image RightPanel;
	public Image DownPanel;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		panelCenterPosition = transform.position;
	}

	void Update () {
		Plane plane = new Plane (transform.forward,transform.position);
		float enterDis;
		if (plane.Raycast (TouchManager.Instance.PrimaryRay, out enterDis)) {
			worldPoint = TouchManager.Instance.PrimaryRay.GetPoint (enterDis);
			UpdatePointIndicator ();
			CheckPanel ();
			UpdatePanel ();
		}
	}
	void UpdatePointIndicator(){
		Vector3 relativePos = worldPoint - panelCenterPosition;
		if (relativePos.magnitude>panelRestrictionRadius) {
			relativePos = relativePos.normalized * panelRestrictionRadius;
		}
		PointIndicator.transform.position = panelCenterPosition + relativePos;
	}
	bool CheckPanel(){
		_pointingDirection = Direction.None;
		Vector3 relativePos = worldPoint - panelCenterPosition;

		if (relativePos.magnitude < nullifiedRadius) {
			return false;
		} else {
			float angle = GetAngle (new Vector3 (-1,1,0), relativePos);

			if (angle >= 0 && angle<90) {
				_pointingDirection = Direction.Left;
			}
			if(angle >= 90 && angle < 180){
				_pointingDirection = Direction.Down;
			}
			if (angle >= -180 && angle < -90) {
				_pointingDirection = Direction.Right;
			}
			if(angle >=-90 && angle<0){
				_pointingDirection = Direction.Up;
			}

//			Debug.Log ("RelativePos is : " + relativePos + "; Angle is : " + angle);
			return true;
		}
		return false;
	}
	void UpdatePanel(){
		LeftPanel.CrossFadeAlpha (0.3f,0,true);
		UpPanel.CrossFadeAlpha (0.3f,0,true);
		RightPanel.CrossFadeAlpha (0.3f,0,true);
		DownPanel.CrossFadeAlpha (0.3f,0,true);
		switch(_pointingDirection){
		case Direction.Left:
			LeftPanel.CrossFadeAlpha (1.0f,0,true);
			break;
		case Direction.Up:
			UpPanel.CrossFadeAlpha (1.0f,0,true);
			break;
		case Direction.Right:
			RightPanel.CrossFadeAlpha (1.0f,0,true);
			break;
		case Direction.Down:
			DownPanel.CrossFadeAlpha (1.0f,0,true);
			break;
		case Direction.None:
			break;
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawCube (panelCenterPosition,3*Vector3.one);
		Gizmos.color = Color.green;
		Gizmos.DrawCube (worldPoint,1*Vector3.one);
	}
	public static float GetAngle(Vector3 v1, Vector3 v2)
	{
		float sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
		return Vector2.Angle(v1, v2) * sign;
	}
	public enum Direction{Up,Down,Left,Right,None}
}

	