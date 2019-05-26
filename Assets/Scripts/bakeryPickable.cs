using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakeryPickable : MonoBehaviour
{
	public PickManager picker;

	public Sprite baguetteSprite;
    public Sprite flourSprite;

    public bool baguette = false;

    void Update()
	{
		if (baguette == false && GameManager.GetVariable("item_baguette") == 1)
        {
            picker.EmptySlot(flourSprite);
            picker.SetSlot(baguetteSprite);
            baguette = true;
        }
	}
}
