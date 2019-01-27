using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{

	public Animation fadeOutAnimation;
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

			if (inside)
			{
				StartCoroutine(LoadScene(2));
			}
			else
			{
				GameManager.instance.spawnOnBed = false;
				StartCoroutine(LoadScene(1));
			}
		}
    }

	IEnumerator LoadScene(int id)
	{
		GetComponent<AudioSource>().Play();
		fadeOutAnimation.Play("Door_FadeOut");
		yield return new WaitUntil(() => fadeOutAnimation.isPlaying == false);
		SceneManager.LoadScene(id);
	}
}
