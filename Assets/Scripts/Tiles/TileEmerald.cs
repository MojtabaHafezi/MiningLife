using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEmerald: BasicTile
{

	void Start ()
	{
		value = 125;
		health = Random.Range (12, 17);
		difficulty = 30f;
		minTime = 4.5f;
		maxTime = 10f;
		id = CONSTANTS.EMERALD_ID;
		name = CONSTANTS.EMERALD;
	}
}
