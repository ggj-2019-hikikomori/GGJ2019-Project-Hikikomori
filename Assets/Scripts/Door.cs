﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public bool inside;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && GameManager.ActionButton())
		{
			GameManager.instance.anxietyLevel = other.GetComponent<AnxietyManager>().anxietyLevel;
			if (inside)
				SceneManager.LoadScene(1);
			else
				SceneManager.LoadScene(0);
		}
	}
}
