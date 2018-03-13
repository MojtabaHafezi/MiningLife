using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentFiller : MonoBehaviour
{

	public GameObject itemPanel;
	public GameObject capacityText;
	public Sprite[] imageArrays = new Sprite[CONSTANTS.MAXITEMS];


	private GameManager gameManager;




	void Start ()
	{
		gameManager = GameManager.instance;

		UpdatePanels ();

	}

	public void UpdatePanels ()
	{
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
	
			GameObject panel = Instantiate (itemPanel);
			panel.transform.SetParent (this.gameObject.transform, false);
			//panel.transform.parent = this.gameObject.transform;
			//the background image of the panel is the image[0]...
			Image[] images = panel.GetComponentsInChildren<Image> ();
			images [1].sprite = imageArrays [i];
			Text text = panel.GetComponentInChildren<Text> ();
			text.text = CONSTANTS.ReturnNameForId (i);
			text.text += ": " + gameManager.inventory.itemList [i];
			this.gameObject.GetComponent<VerticalLayoutGroup> ().childControlHeight = true;
			
		}
			
	}

	public void DropItem (int id)
	{

		gameManager.inventory.ReduceFromList (id);
		gameManager.inventory.SaveData ();
	}

	public void DropAllItems (int id)
	{
		gameManager.inventory.ReduceCompleteFromList (id);
		gameManager.inventory.SaveData ();
	}

}
