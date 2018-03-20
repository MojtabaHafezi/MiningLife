using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDiamond : BasicTile
{

	void Start ()
	{
		value = 250;
		health = Random.Range (16, 25);
		difficulty = 40f;
		minTime = 6f;
		maxTime = 13f;
		id = CONSTANTS.DIAMOND_ID;
		name = CONSTANTS.DIAMOND;
	}

	public override int GetValue ()
	{
		return value;
	}

}
