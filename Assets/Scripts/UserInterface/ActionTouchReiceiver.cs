using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class ActionTouchReiceiver : Singleton<ActionTouchReiceiver> {

	// Use this for initialization
	public delegate void OnMeleeAttackActions(Vector3 attackDirection);
	public delegate void OnMeleeChargeModeStarttActions();
	public delegate void OnMeleeChargeActionBeganActions();
	public delegate void OnShootActions(Vector3 shootDirection);
	public delegate void OnLaunchModeStartActions();
	public delegate void OnLaunchActions(Vector3 launchDirection);
	public static event OnMeleeAttackActions OnMeleeAttack;
	public static event OnMeleeChargeModeStarttActions OnMeleeChargeModeStart;
	public static event OnMeleeChargeActionBeganActions OnMeleeChargeActionBegan;
	public static event OnShootActions OnShoot;
	public static event OnLaunchModeStartActions OnLaunchModeStart;
	public static event  OnLaunchActions OnLaunch; 
	public float holdThreshold = 0.2f;
	public float meleeRange = 10f;
	private PlayerActionManager playerActionManager;
	private Transform playerTrans;

	private Vector3 _worldPoint;
	public Vector3 WorldPoint {
		get{
			return _worldPoint;
		}
		set{ 
			_worldPoint = value;
		}
	}
	private bool isMeele = false;
	private bool isHolding = false;
	void Start () {
		playerActionManager = PlayerActionManager.Instance;
		playerTrans = playerActionManager.transform;
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnTouchMoved(Vector3 point){
		_worldPoint = point;
		if (isMeele && isHolding) {
			
		}
	}
	void OnTouchStay(Vector3 point){
		_worldPoint = point;
	}
	void OnTouchDown(Vector3 point){
		_worldPoint = point;
		isMeele = PointIsInMeleeRange (point);
		StartCoroutine ("COCallingHoldMode");
	}
	void OnTouchExit(Vector3 point){
		StopCoroutine ("COCallingHoldMode");
		ExecuteAction (point);
	}
	void OnTouchUp(Vector3 point){
		StopCoroutine ("COCallingHoldMode");
		ExecuteAction (point);
	}

	bool PointIsInMeleeRange(Vector3 point){
		return (playerTrans.position - point).magnitude < meleeRange;
	}

	IEnumerator COCallingHoldMode(){
		float count = 0; 
		isHolding = false;
		while (count < holdThreshold) {
			count += Time.deltaTime;
			yield return null;
		}
		isHolding = true;
		Debug.Log ("Charging Tap detected, and the values are : isHoding - " + isHolding + " ; isMelee = " + isMeele + " ;");
		if (isMeele) {
			if(OnMeleeChargeModeStart!=null){
				OnMeleeChargeModeStart ();
			}
		} else {
			if (OnLaunchModeStart != null) {
				OnLaunchModeStart ();
			}
		}
	}
	void ExecuteAction(Vector3 touchExitPoint){
		Debug.Log ("Executing action, and the values are : isHoding = " + isHolding + " ; isMelee = " + isMeele + " ;");
		if (isMeele) {
			if (isHolding) {
				if (OnMeleeChargeActionBegan != null) {
					OnMeleeChargeActionBegan ();
				} 
			} else {
				if (OnMeleeAttack != null) {
					OnMeleeAttack (touchExitPoint);
				}
			}
		} else {
			if (isHolding) {
				OnLaunch (_worldPoint);
			} else {
				OnShoot (_worldPoint);
			}
		}
	}
	void OnDrawGizmos(){
		playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
		Color c = Color.cyan;
		c.a = 0.1f;
		Gizmos.color = c;
		Gizmos.DrawSphere (playerTrans.position,meleeRange);
	}
}
