using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpensesScript : MonoBehaviour
{
	public Text newWealth;
	public Text guildReward;


	void Start ()
	{
		newWealth.text = CONSTANTS.NEWWEALTH + GameManager.instance.currency;
		guildReward.text = CONSTANTS.REWARD + GameManager.instance.lastReward;
	}


}
