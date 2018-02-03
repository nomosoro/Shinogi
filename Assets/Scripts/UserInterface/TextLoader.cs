using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextLoader : MonoBehaviour {

	Text textComponent;
	// Use this for initialization
	public delegate void OnLoadingFinishedActions();
	public event OnLoadingFinishedActions OnLoadingFinished;
	[Range(0.1f,100f)]public float textSpeed = 10;
		//textSpeed means how many character will be displayed per second.
	public string text;
	public bool loadFinished{ get; set;}
	public string Prefix ="";
	private int loadIndex = 0; 
	void Awake () {
		textComponent = GetComponent<Text> ();
	}
	
	// Update is called once per frame

	public void StartLoading(){
		textComponent.text = Prefix;
		loadFinished = false;
		StartCoroutine ("COLoadTextToIndex",text.Length-1);
	}
	public void StopLoading(){
		StopCoroutine ("COLoadTextToIndex");
	}
	public void ResumeLoading(){
		StartCoroutine ("COLoadTextToIndex",text.Length-1);
	}
	public void FinishLoading(){
		loadFinished = true;
		StopLoading ();
		textComponent.text = Prefix+text;
		if (OnLoadingFinished != null) {
			OnLoadingFinished ();
		}
	}
	public void Next(string t){
		text = t;
		StartLoading ();
	}
	IEnumerator COLoadTextToIndex(int targetIndex){
		loadIndex = textComponent.text.Length;
		int startIndex = loadIndex;
		float timeCount = 0;
		while (loadIndex < targetIndex && targetIndex <= text.Length) {
			textComponent.text = Prefix+text.Substring (0, loadIndex);
			timeCount += Time.deltaTime;
			loadIndex = startIndex + (int)Mathf.Floor(timeCount * textSpeed);
			yield return null;
		}
		textComponent.text = Prefix+text.Substring (0, targetIndex);
		if (targetIndex >= text.Length-1) {
			FinishLoading ();
		}
	}
}
