using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPrefabs : MonoBehaviour
{

	public SoundManager soundManager;
	public GameManager gameManager;
	// Use this for initialization
	void Start ()
	{
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}



}
