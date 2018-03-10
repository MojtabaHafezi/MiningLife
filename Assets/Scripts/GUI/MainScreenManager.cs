using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{
	
	public Button exitButton;
	private SoundManager soundManager;
	private GameManager gameManager;

	void Start ()
	{

		gameManager = GameManager.instance;
		soundManager = SoundManager.instance;
		/*
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		*/
		SetButtonListeners ();
	}
	//add the listeners to the buttons manually
	private void SetButtonListeners ()
	{


		exitButton.onClick.AddListener (soundManager.PlayMenu);
		exitButton.onClick.AddListener (gameManager.SaveGameData);
		exitButton.onClick.AddListener (gameManager.LoadMenuScene);
	}
}
