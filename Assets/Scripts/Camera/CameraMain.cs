using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
	public Transform target;

	protected float shakingValue = 0.0f;

	public void setShakingValue(float value)
	{
		shakingValue = value;
	}
}
