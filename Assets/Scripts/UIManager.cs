using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class UIManager : Singleton<UIManager> {

	public GameObject MeleeChargePanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowMeleeChargePanel(){
		MeleeChargePanel.SetActive (true);
	}
	public void HideMeleeChargePanel(){
		MeleeChargePanel.SetActive (false);
	}
}
