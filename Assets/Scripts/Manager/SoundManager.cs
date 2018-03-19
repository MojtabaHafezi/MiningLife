using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public AudioClip walk1;
	public AudioClip walk2;
	public AudioClip mine3;
	public AudioClip mine4;
	public AudioClip menu;
	public AudioClip death;
	public Slider audioSlider;
	public Slider soundSlider;
	private GameManager gameManager;
	public AudioSource audio;
	public AudioSource sound;

	public float soundVolume;
	public float audioVolume;
	


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
		sound.loop = false;
		sound.playOnAwake = false;
		audio.loop = true;
		audio.playOnAwake = true;
		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();

		}
	}

	void OnLevelWasLoaded ()
	{
		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();
		}
	}

	public void PlayFootstep ()
	{
		sound.loop = false;
		int choice = Random.Range (0, 1);
		switch (choice) {
		case 0:
			sound.clip = walk1;
			break;
		case 1:
			sound.clip = walk2;
			break;
		default:
			break;
		}
		if (!sound.isPlaying) {
			sound.Play ();
		}
	}

	public void PlayDeath ()
	{
		sound.loop = false;
		sound.clip = death;
		if (!sound.isPlaying)
			sound.Play ();
	}

	public void PlayMine ()
	{	
		sound.loop = true;
		int choice = Random.Range (0, 2);
		switch (choice) {
		case 0:
			sound.clip = mine4;
			break;
		case 1:
			sound.clip = mine3;
			break;
		default:
			break;
		}
		if (!sound.isPlaying) {
			sound.Play ();
		}
	}

	public void StopLoop ()
	{
		sound.loop = false;
	}

	public void PlayMenu ()
	{
		sound.clip = menu;
		if (!sound.isPlaying) {
			sound.Play ();
		}
	}


	public void LoadOptions ()
	{
		audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
		soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();

		GameManager.instance.LoadGameData ();
		sound.volume = soundVolume;
		soundSlider.value = soundVolume;
		audio.volume = audioVolume;
		audioSlider.value = audioVolume;
	}

	public void SaveOptions ()
	{
		sound.volume = soundSlider.value;
		audio.volume = audioSlider.value;

		soundVolume = sound.volume;
		audioVolume = audio.volume;
		GameManager.instance.SaveGameData ();
	}


}
