using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickaxe : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;
	public Sprite[] sprites;

	//Changes sprite according to the mining efficiency (only changable by buying pickaxes)
	void Start ()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		ChangeSprite ();
	}

	private void ChangeSprite ()
	{
		int eff = GameManager.instance.efficiency;
		switch (eff) {
		case 0:
			break;
		case CONSTANTS.BRONZE_EFF:
			spriteRenderer.sprite = sprites [0];
			break;
		case CONSTANTS.IRON_EFF:
			spriteRenderer.sprite = sprites [1];
			break;
		case CONSTANTS.SILVER_EFF:
			spriteRenderer.sprite = sprites [2];
			break;
		case CONSTANTS.GOLD_EFF:
			spriteRenderer.sprite = sprites [3];
			break;
		case CONSTANTS.DIAMOND_EFF:
			spriteRenderer.sprite = sprites [4];
			break;
		}
		
	}
}
