using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groceryPickable : MonoBehaviour {

	public GameObject smallFlour;
	public GameObject bigFlour;
	public GameObject jam;
	
	void Update () {
		if (GameManager.GetVariable("item_jam") == 1)
			jam.SetActive(true);
		if (GameManager.GetVariable("item_flour_small") == 1)
			smallFlour.SetActive(true);
		if (GameManager.GetVariable("item_flour_big") == 1)
			bigFlour.SetActive(true);
	}
}
