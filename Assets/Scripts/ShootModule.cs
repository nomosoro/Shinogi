using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootModule : MonoBehaviour {

	// Use this for initialization
	public GameObject bullet;
	public Vector3 Origin;
	public float Offset;
	public float speed;
	[HideInInspector]public Vector3 Direction;
	public float cooldownTime;
	private bool cooldown = true;
	// Update is called once per frame
	void Update () {
		
	}
	public void Fire(){
		if (cooldown) {
			GameObject newBullet = Instantiate (bullet,transform.position+Origin+Direction.normalized*Offset,Quaternion.identity);
			newBullet.SendMessage ("SetVelocity",Direction*speed,SendMessageOptions.DontRequireReceiver);
			StartCoroutine ("COCooldown");
		}
	}
	IEnumerator COCooldown(){
		cooldown = false;
		yield return new WaitForSeconds (cooldownTime);
		cooldown = true;
	}
}
