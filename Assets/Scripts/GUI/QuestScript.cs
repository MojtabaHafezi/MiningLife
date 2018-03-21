using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
	public Text text;
	public int depth;
	public int reward;

	// Use this for initialization
	void Start ()
	{
		GenerateQuest ();
		text.text = CONSTANTS.CURRENTTASK;
		text.text += "\n" + CONSTANTS.DEPTH + GameManager.instance.depth;
		text.text += "\n" + CONSTANTS.REWARD + GameManager.instance.reward;
	}

	//Sets a random depth and reward depending on the mining efficiency.
	private void GenerateQuest ()
	{
		if (depth == 0 && GameManager.instance.depth == 0) {
			switch (GameManager.instance.efficiency) {
			case CONSTANTS.BRONZE_EFF:
				depth = Random.Range (10, 25);
				reward = Random.Range (15, 35);
				break;
			case CONSTANTS.IRON_EFF: 
				depth = Random.Range (10, 30);
				reward = Random.Range (15, 45);
				break;
			case CONSTANTS.SILVER_EFF:
				depth = Random.Range (30, 60);
				reward = Random.Range (65, 145);
				break;
			case CONSTANTS.GOLD_EFF:
				depth = Random.Range (60, 90);
				reward = Random.Range (165, 345);
				break;
			default: 
				depth = Random.Range (10, 20);
				reward = Random.Range (15, 35);
				break;
			}

			GameManager.instance.depth = depth;
			GameManager.instance.reward = reward;
			depth = 0;
		}
	}
}
