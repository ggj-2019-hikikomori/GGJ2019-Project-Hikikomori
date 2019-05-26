using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousedelete : MonoBehaviour
{
	void Start ()
	{
		if (GameManager.GetVariable("dialog_11") == 1)
			Destroy(gameObject);
	}
}
