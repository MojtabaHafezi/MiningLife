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

	private void InitialiseGame ()
	{
		if (SceneManager.GetActiveScene ().name == "scene_main")
			boardManager.Initialise ();
	}
}
