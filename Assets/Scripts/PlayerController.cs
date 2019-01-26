using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public float speed;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 velocity = new Vector3(x, 0.0f, z).normalized * speed * Time.fixedDeltaTime;

		transform.LookAt(transform.position + velocity);
		rb.MovePosition(transform.position + velocity);
	}
}
