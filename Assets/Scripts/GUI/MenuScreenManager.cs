using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreenManager : MonoBehaviour
{

	public Button shopButton;
	public Button guildButton;
	public Button caveButton;
	public Button exitButton;
	public GameObject expensesPanel;
	public bool showPanel = false;
	private SoundManager soundManager;
	private GameManager gameManager;

	public void OpenShopScene ()
	{
		soundManager.PlayMenu ();
		gameManager.LoadShopScene ();
	}

	public void OpenGuildScene ()
	{
		soundManager.PlayMenu ();
		gameManager.LoadGuildScene ();
	}

	public void OpenMainScene ()
	{
		soundManager.PlayMenu ();
		gameManager.LoadGameScene ();
	}

	public void OpenStartScreen ()
	{
		SoundManager.instance.PlayMenu ();
		GameManager.instance.SaveGameData ();
		GameManager.instance.LoadStartScene ();
	
	}



	void Start ()
	{
		gameManager = GameManager.instance;
		soundManager = SoundManager.instance;
		/*
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		*/
		SetButtonListeners ();
		showPanel = GameManager.instance.exitFromCave;
		expensesPanel.gameObject.SetActive (showPanel);
	}

	private void SetButtonListeners ()
	{
		shopButton.onClick.AddListener (OpenShopScene);	
		guildButton.onClick.AddListener (OpenGuildScene);
		caveButton.onClick.AddListener (OpenMainScene);
		exitButton.onClick.AddListener (OpenStartScreen);
	}

	public void SetPanelState ()
	{
		GameManager.instance.ExitFromCave ();
		showPanel = !showPanel;
		expensesPanel.gameObject.SetActive (showPanel);

	}


		
}
