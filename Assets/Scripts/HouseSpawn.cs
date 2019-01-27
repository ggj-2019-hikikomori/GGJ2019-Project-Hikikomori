using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawn : MonoBehaviour {

	public Transform bedSpawn;
	public Transform doorSpawn;

	public GameObject player;

	// Use this for initialization
	void Start () {
		if (GameManager.instance.houseSpawn == GameManager.HouseSpawn.bed)
			player.transform.position = bedSpawn.position;
		else
			player.transform.position = doorSpawn.position;
	}
}
