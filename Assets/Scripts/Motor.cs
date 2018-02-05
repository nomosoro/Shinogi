using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Motor : MonoBehaviour {

	// Use this for initialization
	public float acceleration;
	public float maxSpeed;
	public float damp;
	public float turningSpeed;
	public float jumpHeight;
	public Vector3 surfaceNormal = Vector3.up;
	private Vector3 moveDir;
	public Vector3 MoveDir{ 
		get{ 
			return moveDir;
		}
		set{
			value.y = 0;
			moveDir = value.normalized;
		}
	}
	[HideInInspector]public bool IsLookingAt = false;
	public bool IsFreezed{ get; set;}
	public bool IsGrounded{ 
		get {
			RaycastHit hitInfo;
			isGrounded = Physics.Raycast (transform.position, Physics.gravity, out hitInfo,distToGround + 0.1f);
			GroundHitInfo = hitInfo;
			return isGrounded;
		} 
	}
	public RaycastHit GroundHitInfo{ get; set;}

	private bool disabled = false;
	public bool Disabled  {
		get{
			return disabled;
		} 
		set {
			disabled = value;
		}
	}
	private bool isGrounded = false;
	private float distToGround;
	private Collider collider;
	private Rigidbody rb;
	private Transform lookAtTarget;
	void Awake () {
		collider = GetComponent<Collider> ();
		rb = GetComponent<Rigidbody> ();
		MoveDir = Vector3.zero;
		IsFreezed = false;
		distToGround = collider.bounds.extents.y;
	}
	void Update () {
		if (disabled) {
			return;
		}
		if (MoveDir != Vector3.zero && !IsLookingAt) {
			TurnTo (MoveDir);
		}
		if (IsLookingAt) {
			TurnTo (lookAtTarget.position-transform.position);
		}
	}
	void FixedUpdate(){
		CheckGrounded ();
		Move ();
	}

	void CheckGrounded(){
		if(IsGrounded){
			surfaceNormal = GroundHitInfo.normal;
		}
	}

	public void LookAt(Transform trans){
		IsLookingAt = true;
		lookAtTarget = trans;
	}
	public void StopLooking(){
		IsLookingAt = false;
		lookAtTarget = null;
	}
	void Move(){
		//function exposed to manupulate Motor
		if (IsFreezed == true) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			return;
		}
			//remain the direction input last time. 
		rb.velocity = rb.velocity + MoveDir*acceleration*Time.deltaTime;
		//damp whenever the velocity has its magnitude.
		
		Vector3 surfaceVelocity = Vector3.ProjectOnPlane(rb.velocity,surfaceNormal);
		if (surfaceVelocity.magnitude >= maxSpeed) {
			surfaceVelocity = surfaceVelocity.normalized * maxSpeed;
		}
		if (surfaceVelocity.magnitude <= 0.2) { 
			surfaceVelocity = Vector3.zero;
		}
		rb.velocity = Vector3.Project (rb.velocity, surfaceNormal) + surfaceVelocity;
		rb.angularVelocity = Vector3.zero;
	}
	public void Jump(){
		Debug.Log ("Here Jump, isGrounded : " + isGrounded);
		if (isGrounded) {
			Vector3 surfaceVelocity = Vector3.ProjectOnPlane(rb.velocity,GroundHitInfo.normal);
			rb.velocity = -Mathf.Sqrt(2*jumpHeight*Physics.gravity.magnitude)*Physics.gravity.normalized + surfaceVelocity;
		}
	}
	public void Freeze(){
		IsFreezed = true;
	}
	public void UnFreeze(){
		IsFreezed = false;
	}


	public void TurnTo(Vector3 direction){
		direction.y = 0;
		transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation (direction),turningSpeed);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (transform.position,transform.position+Physics.gravity.normalized*(distToGround + 0.1f));
	}
}
