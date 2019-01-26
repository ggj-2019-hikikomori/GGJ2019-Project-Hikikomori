using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float rotateSpeed;

	void FixedUpdate ()
	{
		transform.Rotate(new Vector3(0.0f, 1.0f * rotateSpeed * Time.fixedDeltaTime, 0.0f));
	}
}
