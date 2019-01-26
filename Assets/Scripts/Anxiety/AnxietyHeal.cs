using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyHeal : MonoBehaviour {
	public void Update()
	{
		GetComponent<AnxietyManager>().anxietyLevel -= 0.3f;
	}
}
