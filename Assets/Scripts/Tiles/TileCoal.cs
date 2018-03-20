using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoal: BasicTile
{

	void Awake ()
	{
		value = 5;
		health = Random.Range (2, 6);
		difficulty = 2f;
		minTime = 0.5f;
		maxTime = 2f;
		id = CONSTANTS.COAL_ID;
		name = CONSTANTS.COAL;

	}


	public override int GetValue ()
	{
		return value;
	}

}
