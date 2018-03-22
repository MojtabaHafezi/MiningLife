using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUpdateScript : MonoBehaviour
{

	public Text text;

	void Update ()
	{
		text.text = CONSTANTS.BACKPACK + GameManager.instance.inventory.currentTotal + " / " + GameManager.instance.inventory.capacity;
	}
}
