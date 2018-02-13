using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
	public AudioClip walk1;
	public AudioClip walk2;
	Random rand;

	// for sound effects - need to know which source it is to change the clips
	private AudioSource audioSource;

	//Singleton pattern
	public static SoundManager instance = null;


	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
		audioSource.loop = false;
		audioSource.playOnAwake = false;
	}

	public void PlayFootstep ()
	{
		int choice = Random.Range (0, 1);
		switch (choice) {
		case 0:
			audioSource.clip = walk1;
			break;
		case 1:
			audioSource.clip = walk2;
			break;
		default:
			break;
		}
		if (!audioSource.isPlaying) {
			audioSource.Play ();
		}
	}
}
