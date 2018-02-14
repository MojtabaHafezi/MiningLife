using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
	public AudioClip walk1;
	public AudioClip walk2;
	public AudioClip mine3;
	public AudioClip mine4;
	public AudioClip menu;
	public AudioClip death;


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
		audioSource.loop = false;
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

	public void PlayDeath ()
	{
		audioSource.loop = false;
		audioSource.clip = death;
		if (!audioSource.isPlaying)
			audioSource.Play ();
	}

	public void PlayMine ()
	{	
		audioSource.loop = true;
		int choice = Random.Range (0, 2);
		switch (choice) {
		case 0:
			audioSource.clip = mine4;
			break;
		case 1:
			audioSource.clip = mine3;
			break;
		default:
			break;
		}
		if (!audioSource.isPlaying) {
			audioSource.Play ();
		}
	}

	public void StopLoop ()
	{
		audioSource.loop = false;
	}

	public void PlayMenu ()
	{
		audioSource.clip = menu;
		if (!audioSource.isPlaying) {
			audioSource.Play ();
		}
	}
}
