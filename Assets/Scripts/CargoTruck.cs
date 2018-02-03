using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoTruck : MonoBehaviour {

	// Use this for initialization
	Motor truckMotor;
	void Start () {
		truckMotor = GetComponent<Motor> ();
	}
	
	// Update is called once per frame
	void Update () {
		truckMotor.MoveDir = transform.forward;
	}
}
