using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : CameraMain
{
	Vector3 basePosition;

	void Start ()
	{
		basePosition = transform.position;
	}

	void FixedUpdate ()
	{
		Vector3 shakingOffset = new Vector3(Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue));

		transform.position = basePosition + shakingOffset;

		transform.LookAt(target);
	}
}
