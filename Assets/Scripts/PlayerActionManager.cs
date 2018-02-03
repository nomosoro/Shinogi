using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class PlayerActionManager : Singleton<PlayerActionManager> {

	// Use this for initialization
	PlayerController playerController;
	void Start () {
		playerController = GetComponent<PlayerController> ();
		ActionTouchReiceiver.OnShoot += ExecuteShootAction;
		ActionTouchReiceiver.OnLaunch += ExecuteLaunchAction;
		ActionTouchReiceiver.OnMeleeAttack += ExecuteMeleeAttack;
		ActionTouchReiceiver.OnMeleeChargeModeStart += StartMeleeChargeMode;
		ActionTouchReiceiver.OnMeleeChargeActionBegan += ExecuteMeleeChargeAction;
	}

	void ExecuteShootAction(Vector3 point){
		playerController.ShootAt (point);
		playerController.AimAt (point);
	}
	void ExecuteLaunchAction(Vector3 point){
		playerController.LaunchAt (point);
		playerController.AimAt (point);
	}
	void ExecuteMeleeAttack(Vector3 attackDirection){
		playerController.MeleeAttack (attackDirection);
	}
	void ExecuteMeleeChargeAction(){
		UIManager.Instance.HideMeleeChargePanel ();
	}
	void StartMeleeChargeMode(){
		UIManager.Instance.ShowMeleeChargePanel ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
