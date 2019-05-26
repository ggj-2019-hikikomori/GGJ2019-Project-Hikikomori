using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickManager : MonoBehaviour {

	public List<Image> inventory;
	public Sprite empty;
	public PlayerController player;
	public bool isPicking = false;

	public Sprite jamSprite;

	private void Start()
	{
		updateInventory();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Pickable") && GameManager.ActionButton() && !isPicking)
			StartCoroutine(pickObjectCoroutine(other));
	}

	public void EmptySlot(Sprite s)
	{
		if (GameManager.instance.item1 == s)
			GameManager.instance.item1 = empty;
		else if (GameManager.instance.item2 == s)
			GameManager.instance.item2 = empty;
		else if (GameManager.instance.item3 == s)
			GameManager.instance.item3 = empty;
		updateInventory();
	}

	public void SetSlot(Sprite s)
	{
		if (GameManager.instance.item1 == empty)
			GameManager.instance.item1 = s;
		else if (GameManager.instance.item2 == empty)
			GameManager.instance.item2 = s;
		else if (GameManager.instance.item3 == empty)
			GameManager.instance.item3 = s;
		updateInventory();
	}

	IEnumerator pickObjectCoroutine(Collider pickedObj)
	{
		isPicking = true;
		player.animator.SetTrigger("Pickup");
		player.isPaused = true;
		yield return new WaitForSeconds(1.0f);

		if (pickedObj.GetComponent<PickableObject>().icon == jamSprite && jamSprite != null)
			GameManager.SetVariable(new GameManager.StoryVariable("item_jam", 1));

		SetSlot(pickedObj.GetComponent<PickableObject>().icon);

		Destroy(pickedObj.gameObject);
		yield return new WaitForSeconds(0.8f);
		player.isPaused = false;
		isPicking = false;
	}

	void updateInventory()
	{
		inventory[0].sprite = GameManager.instance.item1;
		inventory[1].sprite = GameManager.instance.item2;
		inventory[2].sprite = GameManager.instance.item3;
	}
}
