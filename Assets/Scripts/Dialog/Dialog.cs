using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject {

	public string unlocalizedName;

	[Range(0.001f, 0.02f)]
	public float displaySpeedInverse;

	[SerializeField]
	public List<GameManager.StoryVariable> conditions;

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
		public List<GameManager.StoryVariable> variableUpdates;

		[SerializeField]
		public Animation animation;

		[SerializeField]
		public List<Choice> choices;

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
	public struct Choice
	{
		public string name;
		public int target;
	}

	[Serializable]
	public enum Animation
	{
		NONE,
		WAVE,
		GIVE
	}
}
