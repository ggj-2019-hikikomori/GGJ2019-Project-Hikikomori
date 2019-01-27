using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawn : MonoBehaviour
{

	public Transform houseSpawn;
	public Transform bakerySpawn;
	public Transform grocerySpawn;

	GameObject Player;
	
	void Start ()
	{
		if (GameManager.instance.citySpawn == GameManager.CitySpawn.house)
			Player.transform.position = houseSpawn.position;
		else if (GameManager.instance.citySpawn == GameManager.CitySpawn.bakery)
			Player.transform.position = bakerySpawn.position;
		else if (GameManager.instance.citySpawn == GameManager.CitySpawn.grocery)
			Player.transform.position = grocerySpawn.position;
	}
}
