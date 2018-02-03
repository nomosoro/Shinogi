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

	private Vector3 primaryPoint;
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
		primaryPoint = point;
		if (isMeele && isHolding) {
			
		}
	}
	void OnTouchStay(Vector3 point){
		primaryPoint = point;
	}
	void OnTouchDown(Vector3 point){
		primaryPoint = point;
		isMeele = PointIsInMeleeRange (point);
		StartCoroutine ("COCallingHoldMode");
	}
	void OnTouchExit(Vector3 point){
		isHolding = false;
		StopCoroutine ("COCallingHoldMode");
		ExecuteAction (point);
	}
	void OnTouchUp(Vector3 point){
		isHolding = false;
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
		Debug.Log ("Here");
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
				OnLaunch (primaryPoint);
			} else {
				OnShoot (primaryPoint);
			}
		}
	}
	void OnDrawGizmos(){
		playerTrans = PlayerActionManager.Instance.transform;
		Color c = Color.cyan;
		c.a = 0.1f;
		Gizmos.color = c;
		Gizmos.DrawSphere (playerTrans.position,meleeRange);
	}
}
