using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	AudioSource source;
	AudioLowPassFilter lowPass;

	float lastAnxietyLevel;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}

	void Start ()
	{
		source = GetComponent<AudioSource>();
		lowPass = GetComponent<AudioLowPassFilter>();
		lastAnxietyLevel = GameManager.instance.anxietyLevel;
		UpdateAudioParameters();
	}
	
	void Update ()
	{
		if (GameManager.instance.anxietyLevel != lastAnxietyLevel)
		{
			lastAnxietyLevel = GameManager.instance.anxietyLevel;
			UpdateAudioParameters();
		}
	}

	void UpdateAudioParameters()
	{
		source.pitch = Mathf.Lerp(1.0f, 0.7f, Mathf.InverseLerp(70.0f, 100.0f, GameManager.instance.anxietyLevel));
		lowPass.cutoffFrequency = Mathf.Lerp(5000.0f, 300.0f, Mathf.InverseLerp(0.0f, 100.0f, GameManager.instance.anxietyLevel));
	}
}
