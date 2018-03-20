using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuyScript : MonoBehaviour
{

	public Button[] buttons;
	public Text text;

	void Start ()
	{
		UpdateText ();
	}

	public void UpdateText ()
	{
		text.text = CONSTANTS.WEALTH + GameManager.instance.currency;
		Debug.Log (CONSTANTS.WEALTH + GameManager.instance.currency);
	}

	//if player has enough money the transaction is done immediately
	public void BuyItem (int id)
	{
		MakeTransaction (id);
		GameManager.instance.SaveGameData ();
		UpdateText ();
	}

	

	//Make the transaction: reduce currency, set attributes
	private void MakeTransaction (int id)
	{
		switch (id) {
		case 0:
			if (GameManager.instance.currency >= CONSTANTS.BRONZE_PICK) {
				GameManager.instance.currency -= CONSTANTS.BRONZE_PICK;
				GameManager.instance.efficiency = CONSTANTS.BRONZE_EFF;
				GameManager.instance.maxStamina = CONSTANTS.BRONZE_STA;
			}
			break;
		case 1:
			if (GameManager.instance.currency >= CONSTANTS.IRON_PICK) {
				GameManager.instance.currency -= CONSTANTS.IRON_PICK;
				GameManager.instance.efficiency = CONSTANTS.IRON_EFF;
				GameManager.instance.maxStamina = CONSTANTS.IRON_STA;
			}
			break;
		case 2:
			if (GameManager.instance.currency >= CONSTANTS.SILVER_PICK) {
				GameManager.instance.currency -= CONSTANTS.SILVER_PICK;
				GameManager.instance.efficiency = CONSTANTS.SILVER_EFF;
				GameManager.instance.maxStamina = CONSTANTS.SILVER_STA;
			}
			break;
		case 3:
			if (GameManager.instance.currency >= CONSTANTS.GOLD_PICK) {
				GameManager.instance.currency -= CONSTANTS.GOLD_PICK;
				GameManager.instance.efficiency = CONSTANTS.GOLD_EFF;
				GameManager.instance.maxStamina = CONSTANTS.GOLD_STA;
			}
			break;
		case 4: 
			if (GameManager.instance.currency >= CONSTANTS.DIAMOND_PICK) {
				GameManager.instance.currency -= CONSTANTS.DIAMOND_PICK;
				GameManager.instance.efficiency = CONSTANTS.DIAMOND_EFF;
				GameManager.instance.maxStamina = CONSTANTS.DIAMOND_STA;
			}
			break;
	
		}
	}
}
