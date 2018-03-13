using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

	public int[] itemList;
	public int capacity;
	public int currentTotal;


	public Inventory (int maxItems)
	{
		itemList = new int[CONSTANTS.MAXITEMS];
		LoadData ();
	}


	//Persistence
	public void LoadData ()
	{
		capacity = 20; //Load from saved data or default
		currentTotal = 0;
	}

	public void SaveData ()
	{
		
	}

	public void ReduceFromList (int id)
	{
		if (itemList [id] > 0) {
			itemList [id] -= 1;
			currentTotal -= 1;
			capacity += 1;
			SaveData ();
		}

	}

	public void ReduceCompleteFromList (int id)
	{
		capacity += itemList [id];
		currentTotal -= itemList [id];
		itemList [id] = 0;
		SaveData ();
	}

	public void AddToList (int id)
	{
		if (capacity > 0) {
			itemList [id] += 1;
			currentTotal += 1;
			capacity -= 1;
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
