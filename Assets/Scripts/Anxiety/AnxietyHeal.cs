using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyHeal : MonoBehaviour {
	public void Update()
	{
		GameManager.instance.anxietyLevel -= 0.3f;
	}
}
