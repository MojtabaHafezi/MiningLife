using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentFillerShop : MonoBehaviour
{

	//Prefab to be initialised in the content for the scrollview
	public GameObject itemPanel;
	//Text for the backpack capacity and amount of currency
	public Text capacityText;
	public Text currencyText;
	//Array of sprites for the individual resources
	public Sprite[] imageArrays = new Sprite[CONSTANTS.MAXITEMS];
	//Array of texts for each resource's text.
	public Text[] texts = new Text[CONSTANTS.MAXITEMS];
	//List of button array: Since the itemPanel has 2 buttons in it with 2 different
	//methods - this was implemented. See the SetBUttonListener for more details
	private List<Button[]> sellButtons = new List<Button[]> (CONSTANTS.MAXITEMS);

	//Array of scripts for each resource's value.
	public GameObject[] basicTiles;




	private GameManager gameManager;




	void Start ()
	{
		gameManager = GameManager.instance;
		InitialisePanels ();
		//needs to be done separately because delegate delays the call to after the loop
		SetButtonListeners ();

	}

	public void InitialisePanels ()
	{
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {

			GameObject panel = Instantiate (itemPanel);
			panel.transform.SetParent (this.gameObject.transform, false);
			//panel.transform.parent = this.gameObject.transform;
			//the background image of the panel is the image[0]...
			Image[] images = panel.GetComponentsInChildren<Image> ();
			images [1].sprite = imageArrays [i];
			Text text = panel.GetComponentInChildren<Text> ();
			texts [i] = text;
			text.text = CONSTANTS.ReturnNameForId (i);
			text.text += CONSTANTS.ReturnNameForId (i) + ": " + gameManager.inventory.itemList [i] + " / " + CONSTANTS.PRICE + GetValueForId (i);
			Button[] buttons = panel.GetComponentsInChildren<Button> ();
			sellButtons.Add (buttons);
		}
		capacityText.text = CONSTANTS.BACKPACK + GameManager.instance.inventory.currentTotal + " / " + GameManager.instance.inventory.capacity;
		currencyText.text = CONSTANTS.WEALTH + GameManager.instance.currency;

	}

	//Since the addListener method does not take parameters normally
	//this approach was used. The i attribute can't be used for calling the DropItem(i) method
	//because the delegate activates after the counter is through - leading to all items going out of index range.
	public void SetButtonListeners ()
	{
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			int id = ReturnConstId (i);
			Button[] buttons = sellButtons [i];
			buttons [0].onClick.AddListener (delegate {
				SellItem (id);
			});
			buttons [1].onClick.AddListener (delegate {
				SellAllItems (id);
			});
		}
	}

	//taking the counter from the SetBUttonListener method and returning the same as a constant
	private  int ReturnConstId (int id)
	{
		switch (id) {
		case 0:
			return CONSTANTS.COAL_ID;
		case 1:
			return CONSTANTS.IRON_ID;
		case 2:
			return CONSTANTS.SILVER_ID;
		case 3:
			return CONSTANTS.GOLD_ID;
		case 4:
			return CONSTANTS.EMERALD_ID;
		case 5:
			return CONSTANTS.RUBY_ID;
		case 6:
			return CONSTANTS.DIAMOND_ID;
		default:
			return 0;
		}
	}

	//Updates the text for the panel with the correct number of items in the inventory and backpack
	public void UpdatePanels ()
	{
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			texts [i].text = CONSTANTS.ReturnNameForId (i) + ": " + gameManager.inventory.itemList [i] + " / " + CONSTANTS.PRICE + GetValueForId (i);
		}
		capacityText.text = CONSTANTS.BACKPACK + gameManager.inventory.currentTotal + " / " + gameManager.inventory.capacity;
		currencyText.text = CONSTANTS.WEALTH + GameManager.instance.currency;
	}

	public void SellItem (int id)
	{
		if (gameManager.inventory.ReduceFromList (id)) {
			GameManager.instance.currency += GetValueForId (id);
			GameManager.instance.SaveGameData ();
			UpdatePanels ();
		}

	}

	public void SellAllItems (int id)
	{

		int count = 0;
		count = gameManager.inventory.ReduceCompleteFromList (id);
		if (count > 0) {
			GameManager.instance.currency += (count * GetValueForId (id));
			GameManager.instance.SaveGameData ();
			UpdatePanels ();
		}

	
	}


	public  int GetValueForId (int id)
	{
		int value;
		value = basicTiles [id].gameObject.GetComponent<BasicTile> ().GetValue ();
		return value;

	}

}
