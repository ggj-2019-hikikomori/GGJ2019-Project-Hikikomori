using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHouseSetup : MonoBehaviour {

    public DialogProcessor goingOutside;

	void Start () {
        if (!GameManager.instance.spawnOnBed)
            Destroy(goingOutside.gameObject);
    }
}
