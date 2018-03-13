using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ContentFiller : MonoBehaviour
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
	private List<Button[]> dropButtons = new List<Button[]> (CONSTANTS.MAXITEMS);



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
			text.text += ": " + gameManager.inventory.itemList [i];
			Button[] buttons = panel.GetComponentsInChildren<Button> ();
			dropButtons.Add (buttons);
		}
		capacityText.text = "Backpack: " + gameManager.inventory.currentTotal + " / " + gameManager.inventory.capacity;
		currencyText.text = "Wealth: " + gameManager.currency;

	}

	//Since the addListener method does not take parameters normally
	//this approach was used. The i attribute can't be used for calling the DropItem(i) method
	//because the delegate activates after the counter is through - leading to all items going out of index range.
	public void SetButtonListeners ()
	{
		for (int i = 0; i < CONSTANTS.MAXITEMS; i++) {
			int id = ReturnConstId (i);
			Button[] buttons = dropButtons [i];
			buttons [0].onClick.AddListener (delegate {
				DropItem (id);
			});
			buttons [1].onClick.AddListener (delegate {
				DropAllItems (id);
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
			texts [i].text = CONSTANTS.ReturnNameForId (i) + ": " + gameManager.inventory.itemList [i];
		}
			
		capacityText.text = "Backpack: " + gameManager.inventory.currentTotal + " / " + gameManager.inventory.capacity;
	}

	public void DropItem (int id)
	{
		gameManager.inventory.ReduceFromList (id);
		gameManager.inventory.SaveData ();
		UpdatePanels ();
	}

	public void DropAllItems (int id)
	{

		gameManager.inventory.ReduceCompleteFromList (id);
		gameManager.inventory.SaveData ();
		UpdatePanels ();
	}

}
