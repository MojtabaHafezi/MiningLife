using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicTile: MonoBehaviour
{

	public int value { get; protected set; }

	public int id { get; protected set; }

	public string name { get; protected set; }

	public int health{ get; protected set; }

	public float difficulty{ get; protected set; }

	public float minTime { get; protected set; }

	public float maxTime { get; protected set; }


	public float calculateTime (float efficiency)
	{
		float duration = 1f; 
		if (efficiency >= 0) {
			duration = difficulty / efficiency;
		}
		if (duration <= minTime)
			duration = minTime;
		else if (duration >= maxTime)
			duration = maxTime;

		return duration;
	}
		
}
