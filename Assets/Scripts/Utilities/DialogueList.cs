using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSet", menuName = "Customize/DialogueSet", order = 1)]
public class DialogueList : ScriptableObject {
	public string SetName = "New DialogueList";
	public Dialogue[] dialogues;
}
[System.Serializable]
public struct Dialogue
{
	public string speaker;
	[TextAreaAttribute]
	public string text;
}
