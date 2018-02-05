using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		boardManager.Initialise ();
	}
}
