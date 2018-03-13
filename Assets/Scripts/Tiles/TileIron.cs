using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIron : BasicTile
{

	void Start ()
	{
		value = 10;
		health = Random.Range (4, 10);
		difficulty = 6f;
		minTime = 1f;
		maxTime = 4f;
		id = CONSTANTS.IRON_ID;
		name = CONSTANTS.IRON;
	}
}
