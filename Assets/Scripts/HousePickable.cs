using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePickable : MonoBehaviour
{
	public GameObject plants;
	public GameObject mouse;
	public GameObject baguette;

	public PickManager picker;

	public Sprite mouseSprite;
	public Sprite seedSprite;
	public Sprite baguetteSprite;

	void Update ()
	{
		if (GameManager.GetVariable("item_baguette") == 1)
		{
			picker.EmptySlot(baguetteSprite);
			baguette.SetActive(true);
		}
		if (GameManager.GetVariable("item_seeds") == 1)
		{
			picker.EmptySlot(seedSprite);
			plants.SetActive(true);
		}
		if (GameManager.GetVariable("item_mouse") == 1)
		{
			picker.EmptySlot(mouseSprite);
			mouse.SetActive(true);
		}
	}
}
