using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLava: BasicTile
{

	void Start ()
	{
		value = 0;
		health = Random.Range (20, 35);
		difficulty = 12f;
		minTime = 8f;
		maxTime = 15f;
		id = CONSTANTS.LAVA_ID;
		name = CONSTANTS.LAVA;
	}
}