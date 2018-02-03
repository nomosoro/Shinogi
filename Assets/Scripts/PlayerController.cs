using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Motor playerMotor;
	ShootModule gunFireModule;       
	ShootModule grenedeModule;
	public enum ControllerMode{
		Surface,
		TwoD,
		FPS
	}
	public ControllerMode controllerMode;
	// Use this for initialization
	void Start () {
		playerMotor = GetComponent<Motor> ();
		gunFireModule = GetComponents<ShootModule> ()[0];
		grenedeModule = GetComponents<ShootModule> () [1];
		TuneMotor ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (controllerMode) {
		case ControllerMode.Surface:
			playerMotor.MoveDir = new Vector3 (PlayerInput.Instance.MoveHorizontal,0,PlayerInput.Instance.MoveVertical);
			break;
		case ControllerMode.TwoD:	
			playerMotor.MoveDir = new Vector3 (PlayerInput.Instance.MoveHorizontal,0,0);
			break;
		default:
			break;
		}
		if (PlayerInput.Instance.Jump) {
			playerMotor.Jump ();	
		}
		if (PlayerInput.Instance.Shoot) {
			//
		}
	}
	public void AimAt(Vector3 position){
		playerMotor.TurnTo (position-transform.position);
	}
	public void AimTowards(Vector3 position){
		
	}
	void TuneMotor(){
		switch (controllerMode) {
		case ControllerMode.Surface:
			playerMotor.turningSpeed = 1;
			break;
		case ControllerMode.TwoD:	
			playerMotor.turningSpeed = float.PositiveInfinity;
			break;
		default:
			break; 
		}
	}
	public void ShootTowards(Vector3 dir){
		gunFireModule.Direction = dir;
		gunFireModule.Fire ();
	}
	public void ShootAt(Vector3 point){
		gunFireModule.Direction = (point-(transform.position+gunFireModule.Origin)).normalized;
		gunFireModule.Fire ();
	}
	public void LaunchTowards(Vector3 dir){
		grenedeModule.Direction = dir;
		grenedeModule.Fire ();
	}
	public void LaunchAt(Vector3 point){
		grenedeModule.Direction = (point-(transform.position+gunFireModule.Origin)).normalized;
		grenedeModule.Fire ();
	}
	public void MeleeAttack(Vector3 point){
		Debug.Log ("OnMeleeAttack");

	}
	void Die(){
		
	}
}
