using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSilver : BasicTile
{

	void Start ()
	{
		value = 25;
		health = Random.Range (6, 14);
		difficulty = 20f;
		minTime = 3.5f;
		maxTime = 7f;
		id = CONSTANTS.SILVER_ID;
		name = CONSTANTS.SILVER;
	}

	public override int GetValue ()
	{
		return value;
	}

}
