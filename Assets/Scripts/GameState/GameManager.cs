using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

	[System.Serializable]
	public struct StoryVariable
	{

		public StoryVariable(string n, int v)
		{
			name = n;
			value = v;
		}
		public string name;
		public int value;
	}

	[System.Serializable]
	public class StoryVariableEvent : UnityEvent<StoryVariable> {}

	public static GameManager instance;

	#region Player Properties
	public float anxietyBaseLevel;
	public float anxietyLevel;
	public float anxietyPerSec;
	public bool isHealing;

	public string currentScene;
	public float posX;
	public float posY;
	public float posZ;

	public Sprite item1;
	public Sprite item2;
	public Sprite item3;
	#endregion

	#region Story Advancement
	public enum HouseSpawn { bed, door };
	public HouseSpawn houseSpawn = HouseSpawn.bed;

	public enum CitySpawn { house, bakery, grocery };
	public CitySpawn citySpawn = CitySpawn.house;

	private Dictionary<string, int> storyVariables;
	public Dialog dialog_empty;
	#endregion

	public StoryVariableEvent variableUpdateEvent;
	public bool isPaused;

	private void Awake()
	{
		if (instance == null) {
			instance = this;
			storyVariables = new Dictionary<string, int>();
			variableUpdateEvent = new StoryVariableEvent();
			Localization.LoadLanguage("en_US");
			variableUpdateEvent.AddListener(OnVariableUpdate);
		} else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);

	}



    public static bool ActionButton()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
    }

	public static void SetVariable(StoryVariable variable)
	{
		instance.storyVariables[variable.name] = variable.value;
		instance.variableUpdateEvent.Invoke(variable);
	}

	public static int GetVariable(string key)
	{
		if (instance.storyVariables.ContainsKey(key))
		{
			return instance.storyVariables[key];
		}
		else
		{
			return 0;
		}

	}

	public void OnVariableUpdate(StoryVariable variable)
	{
		if (variable.name.Equals("anxietySet"))
			anxietyLevel = variable.value;
	}
}
