﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class AnxietyManager : MonoBehaviour
{
	float lastAnxietyLevel;

	public PostProcessingProfile profile;

	public Animation fadeOut;
	bool isRespawning;

	private void Start()
	{
		UpdatePostProcessingProfile();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Keypad0))
			GameManager.instance.anxietyLevel -= 0.2f;
		else if (Input.GetKey(KeyCode.Keypad1))
			GameManager.instance.anxietyLevel += 0.2f;

		GameManager.instance.anxietyLevel = Mathf.Clamp(GameManager.instance.anxietyLevel, 0.0f, 100.0f);

		if(GameManager.instance.anxietyLevel != lastAnxietyLevel)
		{
			lastAnxietyLevel = GameManager.instance.anxietyLevel;
			UpdatePostProcessingProfile();
		}

		if (GameManager.instance.anxietyLevel >= 100.0f && !isRespawning)
			StartCoroutine(RespawnCoroutine());
	}
	
	void UpdatePostProcessingProfile()
	{
		ColorGradingModel.Settings colorGradingSettings = profile.colorGrading.settings;
		colorGradingSettings.basic.saturation = (100 - GameManager.instance.anxietyLevel) / 100.0f;
		profile.colorGrading.settings = colorGradingSettings;

		if (GameManager.instance.anxietyLevel >= 25.0f)
		{
			Camera.main.GetComponentInChildren<CameraMain>().setShakingValue(Mathf.Lerp(0.0f, 0.2f, Mathf.InverseLerp(25.0f, 100.0f, GameManager.instance.anxietyLevel)));
			GrainModel.Settings grainSettings = profile.grain.settings;
			grainSettings.intensity = Mathf.Lerp(0.0f, 1.0f, Mathf.InverseLerp(25.0f, 100.0f, GameManager.instance.anxietyLevel));
			profile.grain.settings = grainSettings;
		}
		else
		{
			Camera.main.GetComponentInChildren<CameraMain>().setShakingValue(0.0f);
			GrainModel.Settings grainSettings = profile.grain.settings;
			grainSettings.intensity = 0.0f;
			profile.grain.settings = grainSettings;
		}
	}

	IEnumerator RespawnCoroutine()
	{
		GetComponent<PlayerController>().isPaused = true;
		isRespawning = true;
		fadeOut.Play();
		yield return new WaitUntil(() => fadeOut.isPlaying == false);
		GameManager.instance.anxietyLevel = 99.9f;
		GameManager.instance.spawnOnBed = true;
		GetComponent<PlayerController>().isPaused = false;
		SceneManager.LoadScene(1);
	}
}