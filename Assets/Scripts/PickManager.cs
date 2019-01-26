﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickManager : MonoBehaviour {

	public List<Image> inventory;
	public Sprite empty;
	public PlayerController player;

	private void Start()
	{
		foreach(var img in inventory)
			img.sprite = empty;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Pickable") && GameManager.ActionButton())
			StartCoroutine(pickObjectCoroutine(other));
	}

	public void EmptySlot(int id)
	{
		inventory[id].sprite = empty;
	}

	IEnumerator pickObjectCoroutine(Collider pickedObj)
	{
		if(pickedObj != null)
		{
			player.animator.SetTrigger("Pickup");
			player.isPaused = true;
			yield return new WaitForSeconds(1.0f);
			foreach (var img in inventory)
			{
				if (img.sprite == empty && pickedObj != null)
				{
					img.sprite = pickedObj.GetComponent<PickableObject>().icon;
					break;
				}
			}
			if(pickedObj != null)
				Destroy(pickedObj.gameObject);
			yield return new WaitForSeconds(0.8f);
			player.isPaused = false;
		}
	}
}
