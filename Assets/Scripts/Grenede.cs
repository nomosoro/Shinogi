using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenede : MonoBehaviour {

	// Use this for initialization
	public float damage;
	public float damageRadius = 10f;
	public float lifeTime=3f;
	public LayerMask damagingLayer;
	private float explosionForce = 100f;
	Rigidbody rb;
	void Start () {
		StartCoroutine ("COExplode");
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

	IEnumerator COExplode(){
		yield return new WaitForSeconds (lifeTime);
		Explode ();
	}

	void OnCollisionEnter(Collision collision){
		if(damagingLayer == (damagingLayer | (1 << collision.gameObject.layer))){
			collision.gameObject.SendMessage ("OnDamage",damage,SendMessageOptions.DontRequireReceiver);
			StopCoroutine ("CODie");
			Explode ();
		}
	}
	void Explode(){
		Collider[] colliders = Physics.OverlapSphere (transform.position,damageRadius);
		Debug.Log (colliders);
		if (colliders!= null && colliders.Length > 0) {
			foreach(Collider c in colliders){
				Vector3 dis = c.gameObject.transform.position - transform.position;
				c.SendMessage ("OnExplosion",new ExplosionData(explosionForce,transform.position,dis.magnitude),SendMessageOptions.DontRequireReceiver);
			}
		}
		Destroy (gameObject);
	}
}

public struct ExplosionData{
	public float force;
	public Vector3 position;
	public float radius;

	public ExplosionData(float f,Vector3 p, float r){
		force = f;
		position = p;
		radius = r;
	}
}
