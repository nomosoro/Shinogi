using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class DialogueManager : Singleton<DialogueManager> {
	//Incharge of all the dialogue to be played.
	public DialogueList dialogueList;
	private Hashtable controllerTable=new Hashtable ();
	private string anchoredSpeakerName;
	private DialogueController anchoredController;
	private int dialogueIndex = 0;
	void Awake () {
	}
	void Start(){
		PlayDialogueList ();
	}
	public void RegisterSpeaker(string speakerName,DialogueController dc){
		controllerTable.Add (speakerName,dc);
	}
	public void DeregisterSpeaker(string speakerName){
		controllerTable.Remove (speakerName);
	}
	// Update is called once per frame
	void Update () {
		if (PlayerInput.Instance.clickDown) {
			if (anchoredController != null) {
				if (!anchoredController.dialogueFinished) {
					anchoredController.ForwindDialogue ();
				} else {
					NextDialogue ();
				}
			}
		}
	}

	public void PlayDialogueList(){
		anchoredSpeakerName = dialogueList.dialogues [0].speaker;
		anchoredController = (DialogueController)controllerTable[anchoredSpeakerName];
		anchoredController.dialogue = dialogueList.dialogues[0];
		anchoredController.ShowDialoguePanel ();
		anchoredController.Play ();
	}
	public void NextDialogue(){
		dialogueIndex++;
		if (dialogueIndex >= dialogueList.dialogues.Length) {
			anchoredController.HideDialoguePanel ();
		} else {
			Dialogue next = dialogueList.dialogues [dialogueIndex];
			if (next.speaker == anchoredSpeakerName) {
				anchoredController.dialogue = next;
				anchoredController.Play ();
			} else {
				if (controllerTable.ContainsKey (next.speaker)) {
					anchoredSpeakerName = next.speaker;
					anchoredController.HideDialoguePanel ();

					anchoredController = (DialogueController)controllerTable [anchoredSpeakerName];
					anchoredController.dialogue = next;
					anchoredController.ShowDialoguePanel ();
					anchoredController.Play ();
				} else {
					NextDialogue ();
				}
			}
		}
	}
}
