using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseUpgrades : MonoBehaviour {

	public GameObject housePlant;

	void Start () {
		GameManager.instance.variableUpdateEvent.AddListener(OnVariableUpdate);
	}


	public void OnVariableUpdate(GameManager.StoryVariable variable) {
		if (variable.name.Equals("UnlockPlant") && variable.value == 1) {
			housePlant.SetActive(true);
		}
	}

	void OnDestroy()
	{
		GameManager.instance.variableUpdateEvent.RemoveListener(OnVariableUpdate);
	}
}
