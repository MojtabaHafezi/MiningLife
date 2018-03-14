using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGold : BasicTile
{

	void Start ()
	{
		value = 75;
		health = Random.Range (10, 16);
		difficulty = 24f;
		minTime = 4f;
		maxTime = 9f;
		id = CONSTANTS.GOLD_ID;
		name = CONSTANTS.GOLD;
	}
}
