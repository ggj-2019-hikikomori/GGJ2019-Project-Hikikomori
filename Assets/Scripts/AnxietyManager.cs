using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class AnxietyManager : MonoBehaviour
{
	[Range(0, 100)]
	public float anxietyLevel;
	float lastAnxietyLevel;

	public PostProcessingProfile profile;

	public Animation fadeOut;
	bool isRespawning;

	private void Start()
	{
		anxietyLevel = GameManager.instance.anxietyLevel;
		UpdatePostProcessingProfile();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Keypad0))
			anxietyLevel -= 0.2f;
		else if (Input.GetKey(KeyCode.Keypad1))
			anxietyLevel += 0.2f;

		anxietyLevel = Mathf.Clamp(anxietyLevel, 0.0f, 100.0f);

		if(anxietyLevel != lastAnxietyLevel)
		{
			lastAnxietyLevel = anxietyLevel;
			UpdatePostProcessingProfile();
		}

		if (anxietyLevel >= 100.0f && !isRespawning)
			StartCoroutine(RespawnCoroutine());
	}
	
	void UpdatePostProcessingProfile()
	{
		ColorGradingModel.Settings colorGradingSettings = profile.colorGrading.settings;
		colorGradingSettings.basic.saturation = (100 - anxietyLevel) / 100.0f;
		profile.colorGrading.settings = colorGradingSettings;

		if (anxietyLevel >= 25.0f)
		{
			Camera.main.GetComponentInChildren<CameraMain>().setShakingValue(Mathf.Lerp(0.0f, 0.2f, Mathf.InverseLerp(25.0f, 100.0f, anxietyLevel)));
			GrainModel.Settings grainSettings = profile.grain.settings;
			grainSettings.intensity = Mathf.Lerp(0.0f, 1.0f, Mathf.InverseLerp(25.0f, 100.0f, anxietyLevel));
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
