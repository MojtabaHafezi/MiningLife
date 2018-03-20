using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRuby : BasicTile
{

	void Start ()
	{
		value = 150;
		health = Random.Range (13, 18);
		difficulty = 35f;
		minTime = 5f;
		maxTime = 11f;
		id = CONSTANTS.RUBY_ID;
		name = CONSTANTS.RUBY;
	}

	public override int GetValue ()
	{
		return value;
	}
}
