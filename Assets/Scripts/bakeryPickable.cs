using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakeryPickable : MonoBehaviour
{
	public PickManager picker;

	public Sprite baguetteSprite;

	void Update()
	{
		if (GameManager.GetVariable("item_baguette") == 1)
			picker.SetSlot(baguetteSprite);
	}
}
