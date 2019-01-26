using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyProjector : MonoBehaviour {

    public float emptyPosition;
    public float fullPosition;

	void Start () {
	}
	

	void Update () {
        float yPosition = Mathf.Lerp(emptyPosition, fullPosition, GameManager.instance.anxietyLevel / 100);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

	}
}
