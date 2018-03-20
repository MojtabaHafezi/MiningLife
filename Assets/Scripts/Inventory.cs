using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

	public int[] itemList;
	public int capacity;
	public int currentTotal;


	public Inventory ()
	{
		itemList = new int[CONSTANTS.MAXITEMS];
	}


	//Persistence
	public void LoadData ()
	{
		capacity = PlayerPrefs.GetInt (CONSTANTS.CAPACITY, 20);
		currentTotal = PlayerPrefs.GetInt (CONSTANTS.CURRENTTOTAL, 0);
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			itemList [i] = PlayerPrefs.GetInt (i + "", 0);
		}

	}

	public void SaveData ()
	{
		PlayerPrefs.SetInt (CONSTANTS.CURRENTTOTAL, currentTotal);
		PlayerPrefs.SetInt (CONSTANTS.CAPACITY, capacity);
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			PlayerPrefs.SetInt (i + "", itemList [i]);
		}

		PlayerPrefs.Save ();
	}

	public void SetDefaultData ()
	{
		capacity = 20;
		currentTotal = 0;
		PlayerPrefs.SetInt (CONSTANTS.CURRENTTOTAL, 0);
		PlayerPrefs.SetInt (CONSTANTS.CAPACITY, 20);
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			itemList [i] = 0;
		}
		PlayerPrefs.Save ();
	}

	public bool ReduceFromList (int id)
	{
		if (itemList [id] > 0) {
			itemList [id] -= 1;
			currentTotal -= 1;
			SaveData ();
			return true;
		}
		return false;

	}

	public int ReduceCompleteFromList (int id)
	{
		int counter = 0;
		currentTotal -= itemList [id];
		counter += itemList [id];
		itemList [id] = 0;
		SaveData ();
		return counter;
	}

	public void AddToList (int id)
	{
		if (currentTotal < capacity) {
			itemList [id] += 1;
			currentTotal += 1;
			SaveData ();
		} else {
			Debug.Log ("SHOW GUI: NOSPACE IN BACKPACK");
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Inventory]" + itemList [CONSTANTS.IRON_ID]);
	}

}
