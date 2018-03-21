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
	public float playerDepth = 0;

	public int currency { get; set; }

	public int stamina{ get; set; }

	public int efficiency { get; set; }

	public int maxStamina{ get; set; }

	//for the guild quest rewards
	public int reward = 0;
	public int lastReward = 0;
	//required depth
	public int depth = 0;
	public bool exitFromCave = false;

	private BoardManager boardManager;
	public Inventory inventory;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		inventory = new Inventory ();
		exitFromCave = false;
	}

	void Start ()
	{
		boardManager = GetComponent<BoardManager> ();
		LoadGameData ();
		InitialiseGame ();
	
	}

	void Update ()
	{
		if (SceneManager.GetActiveScene ().name == CONSTANTS.MAINSCENE) {
			if (player != null) {
				playerDepth = player.transform.position.y;
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
			exitFromCave = false;
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
		SaveLoadManager.instance.SaveAllData ();
	}

	public void LoadGameData ()
	{
		SaveLoadManager.instance.LoadAllData ();
	}

	public void SetDefaultData ()
	{
		SaveLoadManager.instance.SetDefaultData ();
	}

	public void ExitFromCave ()
	{
		GameManager.instance.exitFromCave = !GameManager.instance.exitFromCave;
	}

	//Reduces the currency by expenses, resets stamina and saves data
	public void PayandRecover ()
	{
		GameManager.instance.stamina = GameManager.instance.maxStamina;
		GameManager.instance.currency -= CONSTANTS.EXPENSES;
		GameManager.instance.CheckForQuest ();

		GameManager.instance.SaveGameData ();
	}


	private void CheckForQuest ()
	{
		// if the depth was set
		if (GameManager.instance.depth != 0) {
			//if the player reached the depth
			if (Mathf.Abs (GameManager.instance.playerDepth) + 8 >= GameManager.instance.depth) {
				GameManager.instance.currency += reward;
				//by setting these back to 0 the script in the guild scene will activate
				GameManager.instance.lastReward = GameManager.instance.reward;
				GameManager.instance.reward = 0;
				GameManager.instance.depth = 0;
			} else {
				GameManager.instance.lastReward = 0;
			}
		} else {
			GameManager.instance.lastReward = 0;
		}
	}


}
