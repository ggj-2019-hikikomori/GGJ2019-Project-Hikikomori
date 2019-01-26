using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraMain
{
	public float smoothSpeed;
	public Vector3 offset;

	void FixedUpdate ()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

		Vector3 shakingOffset = new Vector3(Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue), Random.Range(-shakingValue, shakingValue));

		transform.position = smoothedPosition + shakingOffset;

		transform.LookAt(target);
	}
}
