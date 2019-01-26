using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed;
	public Vector3 offset;

	float shakingValue = 0.0f;

	void FixedUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

		Vector3 shakingOffset = new Vector3(Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue));

		transform.position = smoothedPosition + shakingOffset;

		transform.LookAt(target);
	}

	public void setShakingValue(float value)
	{
		shakingValue = value;
	}
}
