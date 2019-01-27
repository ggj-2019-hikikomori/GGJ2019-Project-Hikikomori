using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
	public bool spawnOnBed = true;

	private Dictionary<string, int> storyVariables;
	public Dialog dialog_empty;
	#endregion

	private void Awake()
	{
		storyVariables = new Dictionary<string, int>();
		Localization.LoadLanguage("fr_FR");
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}

    public static bool ActionButton()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
    }

	public static void SetVariable(string key, int value)
	{
		instance.storyVariables[key] = value;
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
}
