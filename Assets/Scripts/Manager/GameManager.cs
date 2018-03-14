using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Singleton pattern
	public static GameManager instance = null;

	//Attributes for player
	private GameObject player;
	private float playerY = 0;

	public long currency { get; set; }

	public int stamina{ get; set; }

	public int efficiency { get; set; }

	private BoardManager boardManager;
	public Inventory inventory;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

		boardManager = GetComponent<BoardManager> ();
		inventory = new Inventory (CONSTANTS.MAXITEMS);
		InitialiseGame ();
		LoadGameData ();
	}

	void Update ()
	{
		if (SceneManager.GetActiveScene ().name == CONSTANTS.MAINSCENE) {
			if (player != null) {
				if (player.transform.position.y < (playerY - CONSTANTS.TILESAHEAD)) {
					playerY = player.transform.position.y;
					boardManager.AddToList ();
				}
			}
		}

		
	}

	void OnLevelWasLoaded ()
	{
		InitialiseGame ();
	}

	private void InitialiseGame ()
	{
		if (SceneManager.GetActiveScene ().name == CONSTANTS.MAINSCENE) {
			instance.boardManager.Initialise ();
			player = GameObject.FindGameObjectWithTag (CONSTANTS.PLAYER);
			playerY = player.transform.position.y;
		}
	}

	public void LoadMenuScene ()
	{
		instance.StartCoroutine (LoadSceneAsynch (CONSTANTS.MENUSCENE));
	}

	public void LoadGameScene ()
	{
		
		SceneManager.LoadScene (CONSTANTS.MAINSCENE);
	}

	public void LoadGameOverScene ()
	{
		instance.StartCoroutine (LoadSceneAsynch (CONSTANTS.GAMEOVERSCENE));
	}

	public void LoadStartScene ()
	{
		instance.StartCoroutine (LoadSceneAsynch (CONSTANTS.STARTSCENE));
	}

	public void LoadShopScene ()
	{
		instance.StartCoroutine (LoadSceneAsynch (CONSTANTS.SHOPSCENE));

	}

	public void LoadGuildScene ()
	{
		instance.StartCoroutine (LoadSceneAsynch (CONSTANTS.GUILDSCENE));

	}

	public void QuitApplication ()
	{
		Application.Quit ();
	}

	public IEnumerator LoadSceneAsynch (string name)
	{
		// The Application loads the Scene in the background at the same time as the current Scene.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (name);

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone) {
			yield return null;
		}
	}

	//TODO
	public void SaveGameData ()
	{
		inventory.SaveData ();
	}

	//TODO
	public void LoadGameData ()
	{
		//TODO: load data for player
		stamina = 100;
		currency = 0;
		efficiency = 10;
		inventory.LoadData ();
	}


}
