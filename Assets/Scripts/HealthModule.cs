using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour {

	// Use this for initialization
	public float maxHealth;
	private float health;

	void OnHeal(float heal){
		health += heal;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}
	void OnDamage(float damage){
		health -= damage;
		if (health < 0) {
			health = 0;
			SendMessage ("Die");
		}
	}
}
