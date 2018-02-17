using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Singleton pattern
	public static GameManager instance = null;

	private BoardManager boardManager;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

		boardManager = GetComponent<BoardManager> ();

		InitialiseGame ();

	}

	void OnLevelWasLoaded ()
	{
		InitialiseGame ();
	}

	private void InitialiseGame ()
	{
		if (SceneManager.GetActiveScene ().name == "scene_main")
			boardManager.Initialise ();
	}

	public void LoadMenuScene ()
	{
		StartCoroutine (LoadSceneAsynch ("scene_menu"));
	}

	public void LoadGameScene ()
	{
		SceneManager.LoadScene ("scene_main");
	}

	public void LoadGameOverScene ()
	{
		StartCoroutine (LoadSceneAsynch ("scene_gameover"));
	}

	public void LoadStartScene ()
	{
		StartCoroutine (LoadSceneAsynch ("scene_start"));
	}

	public void LoadShopScene ()
	{
		StartCoroutine (LoadSceneAsynch ("scene_shop"));

	}

	public void LoadGuildScene ()
	{
		StartCoroutine (LoadSceneAsynch ("scene_guild"));

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
		
	}

	//TODO
	public void LoadGameData ()
	{
		
	}


}
