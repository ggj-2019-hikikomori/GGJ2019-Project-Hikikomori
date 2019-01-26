using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	public float speed;

	#region Inputs

	private Vector3 movementInput;

	private void Inputs() {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		movementInput = new Vector3(x, 0.0f, z).normalized;
	}

	#endregion

	Rigidbody rb;
	Animator animator;

	float velocity;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		movementInput = Vector3.zero;
	}

	void Update()
	{
		Inputs();
		UpdateAnimatorParameters();
	}

	void FixedUpdate ()
	{
		Vector3 velocity = movementInput * speed * Time.fixedDeltaTime;
		this.velocity = velocity.magnitude;

		transform.LookAt(transform.position + velocity);
		rb.MovePosition(transform.position + velocity);
	}

	void UpdateAnimatorParameters()
	{
		animator.SetFloat("Velocity", velocity);
	}


}
