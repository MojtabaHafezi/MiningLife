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
		instance.audioSource = GetComponent<AudioSource> ();
		instance.audioSource.loop = false;
		instance.audioSource.playOnAwake = false;
	}

	public void PlayFootstep ()
	{
		instance.audioSource.loop = false;
		int choice = Random.Range (0, 1);
		switch (choice) {
		case 0:
			instance.audioSource.clip = walk1;
			break;
		case 1:
			instance.audioSource.clip = walk2;
			break;
		default:
			break;
		}
		if (!instance.audioSource.isPlaying) {
			instance.audioSource.Play ();
		}
	}

	public void PlayDeath ()
	{
		instance.audioSource.loop = false;
		instance.audioSource.clip = death;
		if (!instance.audioSource.isPlaying)
			instance.audioSource.Play ();
	}

	public void PlayMine ()
	{	
		instance.audioSource.loop = true;
		int choice = Random.Range (0, 2);
		switch (choice) {
		case 0:
			instance.audioSource.clip = mine4;
			break;
		case 1:
			instance.audioSource.clip = mine3;
			break;
		default:
			break;
		}
		if (!instance.audioSource.isPlaying) {
			instance.audioSource.Play ();
		}
	}

	public void StopLoop ()
	{
		instance.audioSource.loop = false;
	}

	public void PlayMenu ()
	{
		instance.audioSource.clip = menu;
		if (!instance.audioSource.isPlaying) {
			instance.audioSource.Play ();
		}
	}

}
