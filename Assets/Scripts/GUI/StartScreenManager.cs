using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{

	public Button newGameButton;
	public Button loadGameButton;
	public Button optionButton;
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
		newGameButton.onClick.AddListener (soundManager.PlayMenu);
		newGameButton.onClick.AddListener (gameManager.LoadMenuScene);

		loadGameButton.onClick.AddListener (soundManager.PlayMenu);
		loadGameButton.onClick.AddListener (gameManager.LoadGameData);
		loadGameButton.onClick.AddListener (gameManager.LoadMenuScene);

		optionButton.onClick.AddListener (soundManager.PlayMenu);

		exitButton.onClick.AddListener (soundManager.PlayMenu);
		exitButton.onClick.AddListener (gameManager.QuitApplication);
	}
}
