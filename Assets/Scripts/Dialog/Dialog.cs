using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject {

	[Range(0.001f, 0.02f)]
	public float displaySpeedInverse;

	[SerializeField]
	public List<StoryVariable> conditions;

	[SerializeField]
	public List<DialogStep> dialogSteps = new List<DialogStep>();

	[System.Serializable]
	public struct DialogStep
	{
		public string text;
		public DiaglogFeeling feeling;
		public bool player;
		public bool internalThoughts;

		public GameObject sound;
		public float delayBeforeDisplay;

		[SerializeField]
		public List<StoryVariable> variableUpdates;


		public int next;
	}

	[System.Serializable]
	public enum DiaglogFeeling
	{
		Neutral,
		Good,
		Bad
	}

	[System.Serializable]
	public struct StoryVariable
	{
		public string name;
		public int value;
	}

}
