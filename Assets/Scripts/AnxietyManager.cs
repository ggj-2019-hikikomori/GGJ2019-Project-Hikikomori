using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class AnxietyManager : MonoBehaviour
{
	[Range(0, 100)]
	public float anxietyLevel;
	float lastAnxietyLevel;

	public PostProcessingProfile profile;

	private void Start()
	{
		updatePostProcessingProfile();
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
			updatePostProcessingProfile();
		}
	}
	
	void updatePostProcessingProfile()
	{
		ColorGradingModel.Settings colorGradingSettings = profile.colorGrading.settings;
		colorGradingSettings.basic.saturation = (100 - anxietyLevel) / 100.0f;
		profile.colorGrading.settings = colorGradingSettings;

		if (anxietyLevel >= 25.0f)
		{
			Camera.main.GetComponentInChildren<CameraFollow>().setShakingValue(Mathf.Lerp(0.0f, 0.2f, Mathf.InverseLerp(25.0f, 100.0f, anxietyLevel)));
			GrainModel.Settings grainSettings = profile.grain.settings;
			grainSettings.intensity = Mathf.Lerp(0.0f, 1.0f, Mathf.InverseLerp(25.0f, 100.0f, anxietyLevel));
			profile.grain.settings = grainSettings;
		}
		else
		{
			Camera.main.GetComponentInChildren<CameraFollow>().setShakingValue(0.0f);
			GrainModel.Settings grainSettings = profile.grain.settings;
			grainSettings.intensity = 0.0f;
			profile.grain.settings = grainSettings;
		}
	}
}
