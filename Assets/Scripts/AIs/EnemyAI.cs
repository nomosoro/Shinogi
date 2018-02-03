using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyController))]
public class EnemyAI : MonoBehaviour {
	// Use this for initialization
	public float scanRange;
	public enum EnemyState{
		Patrolling,
		Following,
		Traveling,
		Attacking,
		Resting,
	}
	public EnemyState State{ get; set;}


	private EnemyController enemyController;
	private PlayerController playerController;
	private float stateUpdateInterval;
	private GameObject followingTarget;
	private GameObject player;


	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		State = EnemyState.Patrolling;
		enemyController = GetComponent<EnemyController> ();
		playerController = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	// Update is called once per frame
	void Update(){
		UpdateState ();
	}
	void FixedUpdate () {
		ExecuteState ();
	}

	void UpdateState (){
		switch (State) {
		case EnemyState.Patrolling:
			if (CanSeePlayer()) {
				State = EnemyState.Following;
				followingTarget = player;
			}
			break;
		case EnemyState.Following:
			if(CanAttack()){
				enemyController.Halt ();
				State = EnemyState.Attacking;
			}
			break;
		case EnemyState.Traveling:
			break;
		case EnemyState.Attacking:
			if (!CanAttack ()) {
				State = EnemyState.Following;
				followingTarget = player;
			}
			break;
		case EnemyState.Resting:
			break;
		}
	}


	void ExecuteState (){

		if (State != EnemyState.Patrolling) {
			enemyController.StopPatrol ();
		}
		switch (State) {
		case EnemyState.Patrolling:
			enemyController.Patrol ();
			break;
		case EnemyState.Following:
			if (followingTarget == null) {
				Debug.LogError ("No Following Target to follow at for EnemyAI");
			}
			enemyController.MoveTo (followingTarget.transform.position);
			break;
		case EnemyState.Traveling:
			break;
		case EnemyState.Attacking:
			enemyController.Attack ();
			break;
		case EnemyState.Resting:
			break;
		}
	}

	protected bool CanSeePlayer(){
		return (playerController.transform.position - transform.position).magnitude<=scanRange;
	}
	protected bool CanAttack(){
		return (playerController.transform.position - transform.position).magnitude<=enemyController.AttackRange;
	}
}

