using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Sunset : MonoBehaviour
{
	[Range(0.0f, 100.0f)]
	public float sunsetProgression;
	float lastSunsetProgression;

	public PostProcessingProfile profile;

	void Start ()
	{
		UpdateSunset();
	}
	
	void Update ()
	{
		if (Input.GetKey(KeyCode.Keypad2))
			sunsetProgression -= 0.2f;
		else if (Input.GetKey(KeyCode.Keypad5))
			sunsetProgression += 0.2f;

		sunsetProgression = Mathf.Clamp(sunsetProgression, 0.0f, 100.0f);

		if (sunsetProgression != lastSunsetProgression)
		{
			lastSunsetProgression = sunsetProgression;
			UpdateSunset();
		}
	}

	void UpdateSunset()
	{
		transform.localEulerAngles = new Vector3(Mathf.Lerp(67, 19, sunsetProgression/100.0f), Mathf.Lerp(-60, -35, sunsetProgression / 100.0f), Mathf.Lerp(-30, -12, sunsetProgression / 100.0f));

		ColorGradingModel.Settings colorGradingSettings = profile.colorGrading.settings;
		colorGradingSettings.basic.temperature = sunsetProgression;
		profile.colorGrading.settings = colorGradingSettings;
	}
}
