using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreenManager : MonoBehaviour
{


	public Button buyButton;
	public Button sellButton;
	public Button exitButton;
	private SoundManager soundManager;
	private GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		SetButtonListeners ();
	}
	//add the listeners to the buttons manually
	private void SetButtonListeners ()
	{
		buyButton.onClick.AddListener (soundManager.PlayMenu);
		sellButton.onClick.AddListener (soundManager.PlayMenu);

		exitButton.onClick.AddListener (soundManager.PlayMenu);
		exitButton.onClick.AddListener (gameManager.LoadMenuScene);
	}
}
