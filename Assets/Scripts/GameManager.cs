using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public float anxietyLevel = 0.0f;

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
