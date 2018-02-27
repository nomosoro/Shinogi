using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
public class DialogueController : MonoBehaviour {	
	public Dialogue dialogue;
	public TextLoader textLoader;
	public float showHideTransition = 0.5f;
	public string dialogueSpeakerName;
	public delegate void DialogueFinishedAction();
	public event DialogueFinishedAction OnDialogueFinished;
	public bool dialogueFinished { get; set;}

	public delegate void OnCurrentDialogueTouchedAction();
	public event OnCurrentDialogueTouchedAction OnCurrentDialogueTouched;
	// Use this for initialization
	void Awake(){
		DialogueManager.Instance.RegisterSpeaker (dialogueSpeakerName,this);
	}
	void OnDisabled(){
		DialogueManager.Instance.DeregisterSpeaker (dialogueSpeakerName);
	}

	void Start () {
		dialogueFinished = false;
		textLoader.OnLoadingFinished += OnTextLoaderFinished;
	}
	
	// Update is called once per frame
	public void ShowDialoguePanel(){
		StopCoroutine ("CODialogueHideFade");
		StartCoroutine ("CODialogueShowFade");
	}
	public void HideDialoguePanel(){
		StopCoroutine ("CODialogueShowFade");
		StartCoroutine ("CODialogueHideFade");
	}

	void OnTextLoaderFinished(){
		dialogueFinished = true;
		if (OnDialogueFinished != null) {
			OnDialogueFinished ();
		}
	}
	public void ForwindDialogue(){
		textLoader.FinishLoading ();
	}
	public void Play(){
		dialogueFinished = false;
		textLoader.Prefix = dialogueSpeakerName+" :\n";
		textLoader.Next (dialogue.text);
	}
	IEnumerator CODialogueHideFade(){
		Image dialoguePanel = GetComponent<Image> ();
		Text dialogueText = textLoader.GetComponent<Text> ();
		Color panelColor = dialoguePanel.color;
		Color textColor = dialogueText.color;
		while (panelColor.a > 0||textColor.a>0) {
			if (panelColor.a > 0) {
				panelColor.a -=  Time.deltaTime/showHideTransition ;
			}
			if (textColor.a > 0) {
				textColor.a -= Time.deltaTime/showHideTransition ;
			}
			dialoguePanel.color = panelColor;
			dialogueText.color = textColor;
			yield return null;
		}
	}

	IEnumerator CODialogueShowFade(){
		Image dialoguePanel = GetComponent<Image> ();
		Text dialogueText = textLoader.GetComponent<Text> ();
		Color panelColor = dialoguePanel.color;
		Color textColor = dialogueText.color;
		while (panelColor.a < 1||textColor.a < 1) {
			if (panelColor.a <1) {
				panelColor.a += Time.deltaTime/showHideTransition;
			}
			if (textColor.a <1) {
				textColor.a += Time.deltaTime/showHideTransition;
			}
			dialoguePanel.color = panelColor;
			dialogueText.color = textColor;
			yield return null;
		}
	}

	void OnTouchDown(Vector3 point){
		OnCurrentDialogueTouched ();
	}
}
