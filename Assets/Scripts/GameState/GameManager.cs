using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	#region Player Properties
	public float anxietyBaseLevel;
	public float anxietyLevel;

	public string currentScene;
	public float posX;
	public float posY;
	public float posZ;
	#endregion

	#region Story Advancement
	public bool spawnOnBed = true;
	#endregion

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}


    public static bool ActionButton()
    {
        return Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button0);
    }
}
