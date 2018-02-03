using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchModule : MonoBehaviour {

	// Use this for initialization
	public GameObject projectile;
	public Vector3 Origin;
	public float Offset;
	public float speed;
	[HideInInspector]public Vector3 Direction;
	public float cooldownTime;
	private bool cooldown = true;
	// Update is called once per frame
	void Update () {
		
	}
	public void LaunchAt(){
		if (cooldown) {
			GameObject newBullet = Instantiate (projectile,transform.position+Origin+Direction.normalized*Offset,Quaternion.identity);
			Debug.Log ("here" + newBullet);
			newBullet.GetComponent<Bullet>().SetVelocity(Direction*speed);
			StartCoroutine ("COCooldown");
		}
	}
	IEnumerator COCooldown(){
		cooldown = false;
		yield return new WaitForSeconds (cooldownTime);
		cooldown = true;
	}
}
