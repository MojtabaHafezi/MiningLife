using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStone : BasicTile
{

	void Start ()
	{
		value = 0;
		health = Random.Range (4, 10);
		difficulty = 4f;
		minTime = 0.5f;
		maxTime = 2.5f;
		id = CONSTANTS.STONE_ID;
		name = CONSTANTS.STONE;
	}
}