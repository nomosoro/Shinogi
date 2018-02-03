using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ShootModule))]
public class RangerEnemyController : EnemyController {
	ShootModule shootModule;
	GameObject attackTarget;
	protected virtual void Start(){
		shootModule = GetComponent<ShootModule> ();
		base.Init ();
	}
	public override void Attack(){
		if (attackTarget == null) {
			attackTarget = GameObject.FindGameObjectWithTag ("Player");
		}
		enemyMotor.LookAt (attackTarget.transform);
		PrepareShoot ();
		shootModule.Fire ();
	}
	void PrepareShoot(){
		shootModule.Direction = transform.forward.normalized;
	}
}
