using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileDirt : BasicTile
{

	void Start ()
	{
		value = 0;
		health = Random.Range (2, 6);
		difficulty = 1f;
		minTime = 0.25f;
		maxTime = 1f;
	}


}
