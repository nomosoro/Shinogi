using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour {
	protected Motor enemyMotor;
	public Vector3[] patrolPositions = {};
	public enum ControllerMode{
		Surface,
		TwoD,
		FPS
	}
	public ControllerMode controllerMode;
	public float AttackRange;
	private Vector3 spawnPosition;
	private int patrolIndex = 0;
	private Coroutine patrolCorotine = null;
	// Use this for initialization
	void Start () {
		Init ();
	}
	//For inherited classes, melee enemy and ranger enemy etc.
	protected virtual void Init(){
		spawnPosition = transform.position;
		enemyMotor = GetComponent<Motor> ();
		TuneMotor ();
	}
	
	// Update is called once per frame

	public void Patrol(){
		enemyMotor.StopLooking ();
		if (patrolPositions.Length>0 && patrolCorotine == null) {
			patrolCorotine = StartCoroutine ("COPatrol");
		}
	}

	public void StopPatrol(){
		if (patrolCorotine != null) {
			StopCoroutine (patrolCorotine);
			patrolCorotine = null;
		}
	}
	IEnumerator COPatrol(){
		while (true) {
			if(transform.position.Equals(patrolPositions[patrolIndex])){
				patrolIndex++;
				if (patrolIndex >= patrolPositions.Length) {
					patrolIndex = 0;
				} else {
					MoveTo (spawnPosition + patrolPositions[patrolIndex]);
				}
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
	public void MoveTo(Vector3 targetPosition){
		enemyMotor.MoveDir = targetPosition - transform.position;
	}

	public void Halt(){
		enemyMotor.MoveDir = Vector3.zero;
	}

	public abstract void Attack();


	public void Aim(Vector3 aimTarget){
		
	}
	void TuneMotor(){
		switch (controllerMode) {
		case ControllerMode.Surface:
			enemyMotor.turningSpeed = 1;
			break;
		case ControllerMode.TwoD:	
			enemyMotor.turningSpeed = float.PositiveInfinity;
			break;
		default:
			break;
		}
	}
	void OnExplosion(ExplosionData ed){
		Debug.Log ("exploded! Explosion Info : force - " + ed.force + " ; Position - " + ed.position + " ; Radius - " + ed.radius + " ;");
		GetComponent<Rigidbody> ().AddExplosionForce (ed.force,ed.position,ed.radius);
	}
	void Die(){
		Destroy (gameObject);
	}
}
