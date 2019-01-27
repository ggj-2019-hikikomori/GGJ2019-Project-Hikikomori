using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
	public int actualID;
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
				GameManager.instance.houseSpawn = GameManager.HouseSpawn.door;
				GameManager.instance.isHealing = true;
			}
			else if(levelID == 2)
			{
				GameManager.instance.isHealing = false;
				switch (actualID)
				{
					case 1:
						GameManager.instance.citySpawn = GameManager.CitySpawn.house;
						break;
					case 3:
						GameManager.instance.citySpawn = GameManager.CitySpawn.bakery;
						GameManager.instance.anxietyPerSec = 30;
						break;
					case 4:
						GameManager.instance.citySpawn = GameManager.CitySpawn.grocery;
						break;
				}
			}
			else if(levelID == 3)
			{
				GameManager.instance.anxietyPerSec = 0;
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
