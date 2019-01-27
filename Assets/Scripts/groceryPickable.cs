using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groceryPickable : MonoBehaviour
{
	public GameObject jam;
	public GameObject smallFlour;
	public GameObject bigFlour;

	public PickManager picker;

	public Sprite flourSprite;
	public Sprite jamSprite;
	public Sprite seedSprite;
	
	void Update ()
	{
		if (GameManager.GetVariable("item_jam") == 1)
			jam.SetActive(true);
		if (GameManager.GetVariable("item_flour_small") == 1)
		{
			picker.SetSlot(flourSprite);
			Destroy(smallFlour);
			bigFlour.GetComponent<GlowObjectCmd>().glowActive = false;
		}
		if (GameManager.GetVariable("item_flour_big") == 1)
		{
			picker.SetSlot(flourSprite);
			Destroy(bigFlour);
			smallFlour.GetComponent<GlowObjectCmd>().glowActive = false;
		}
		if (GameManager.GetVariable("item_seeds") == 1)
		{
			picker.EmptySlot(jamSprite);
			picker.SetSlot(seedSprite);
		}

	}
}
