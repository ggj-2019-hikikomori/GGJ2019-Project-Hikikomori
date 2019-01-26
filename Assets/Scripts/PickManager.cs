using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickManager : MonoBehaviour {

	public List<Image> inventory;
	public Sprite empty;

	private void Start()
	{
		foreach(var img in inventory)
			img.sprite = empty;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Pickable") && GameManager.ActionButton())
		{
			//animation
			foreach(var img in inventory)
			{
				if (img.sprite == empty)
				{
					img.sprite = other.GetComponent<PickableObject>().icon;
					break;
				}
			}
			other.GetComponent<PickableObject>().glowActive = false;
			Destroy(other.gameObject);
		}
	}

	public void EmptySlot(int id)
	{
		inventory[id].sprite = empty;
	}
}
