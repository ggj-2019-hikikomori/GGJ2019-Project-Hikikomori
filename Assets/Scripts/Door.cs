using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
	public int levelID;
	public Animation fadeOutAnimation;

    private bool flagActionInput; 

    private void Update()
    {
        flagActionInput = GameManager.ActionButton();
    }

    private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && flagActionInput)
		{
			if(levelID == 1)
			{
				GameManager.instance.spawnOnBed = false;
				GameManager.instance.isHealing = true;
			}
			else
			{
				GameManager.instance.isHealing = false;
			}
			StartCoroutine(LoadScene(levelID));
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
