using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safezone : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		if (c.CompareTag("Player")) GameManager.instance.isHealing = true;
	}

	void OnTriggerExit(Collider c)
	{
		if (c.CompareTag("Player")) GameManager.instance.isHealing = false;
	}
}
