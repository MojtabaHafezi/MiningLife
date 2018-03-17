using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRedStone : BasicTile
{

	void Start ()
	{
		value = 0;
		health = Random.Range (6, 15);
		difficulty = 12f;
		minTime = 2f;
		maxTime = 5.5f;
		id = CONSTANTS.REDSTONE_ID;
		name = CONSTANTS.REDSTONE;
	}
}
