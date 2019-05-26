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

    public bool seeds = false;
	
	void Update ()
	{
		if (jam != null && GameManager.GetVariable("quest_jam") == 1)
			jam.SetActive(true);
		if (smallFlour != null && bigFlour != null && GameManager.GetVariable("item_flour_small") == 1)
		{
			picker.SetSlot(flourSprite);
			Destroy(smallFlour);
			bigFlour.GetComponent<GlowObjectCmd>().glowActive = false;
		}
		if (smallFlour != null && bigFlour != null && GameManager.GetVariable("item_flour_big") == 1)
		{
			picker.SetSlot(flourSprite);
			Destroy(bigFlour);
			smallFlour.GetComponent<GlowObjectCmd>().glowActive = false;
		}
		if (seeds == false && GameManager.GetVariable("item_seeds") == 1)
		{
			picker.EmptySlot(jamSprite);
			picker.SetSlot(seedSprite);
            seeds = true;
		}

	}
}
