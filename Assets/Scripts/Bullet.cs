using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	public LayerMask damagingLayer;
	public float damage;
	public float lifeTime;
	Rigidbody rb;
	void Start () {
		StartCoroutine ("CODie");
		if (rb == null) {
			rb = GetComponent<Rigidbody> ();
		}
	}

	public void SetVelocity(Vector3 v){
		if (rb == null) {
			rb = GetComponent<Rigidbody> ();
		}
		rb.velocity = v;
	}

	IEnumerator CODie(){
		yield return new WaitForSeconds (lifeTime);
		Die ();
	}
	void OnTriggerEnter(Collider collider){
		if(damagingLayer == (damagingLayer | (1 << collider.gameObject.layer))){
			collider.SendMessage ("OnDamage",damage,SendMessageOptions.DontRequireReceiver);
			StopCoroutine ("CODie");
			Die ();
		}
	}
	void Die(){
		Destroy (gameObject);
	}
}
