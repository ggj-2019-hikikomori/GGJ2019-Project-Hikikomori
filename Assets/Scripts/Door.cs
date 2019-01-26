using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public bool inside;

    private bool flagActionInput; 

    private void Update()
    {
        flagActionInput = GameManager.ActionButton();
    }

    private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && flagActionInput)
		{
			GameManager.instance.anxietyLevel = other.GetComponent<AnxietyManager>().anxietyLevel;
			if (inside)
			{
				SceneManager.LoadScene(2);
			}
			else
			{
				GameManager.instance.spawnOnBed = false;
				SceneManager.LoadScene(1);
			}
		}
    }
}
