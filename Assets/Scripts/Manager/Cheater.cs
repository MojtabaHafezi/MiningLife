using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
	public bool enableCheat = false;

	public void Awake ()
	{
		enableCheat = false;
	}

	void ApplyCheat ()
	{
		GameManager.instance.efficiency = CONSTANTS.DIAMOND_EFF;
		GameManager.instance.currency += 10000;
		GameManager.instance.maxStamina = 999999;
		GameManager.instance.stamina = GameManager.instance.maxStamina;
	}

	public void Update ()
	{
		if (enableCheat) {
			ApplyCheat ();
			enableCheat = false;
		}	
	}
}
