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
	public AudioSource audioSource;
	public AudioSource soundSource;
	private AudioSource[] sources;

	public float soundVolume;
	public float audioVolume;
	Random rand;


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
		//get the audioSource components and assign them to the private attributes
		SetAudioSources ();

		SoundManager.instance.soundSource.enabled = true;
		SoundManager.instance.soundSource.loop = false;
		SoundManager.instance.soundSource.playOnAwake = false;
		SoundManager.instance.audioSource.enabled = true;
		SoundManager.instance.audioSource.loop = true;
		SoundManager.instance.audioSource.playOnAwake = true;
		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			SoundManager.instance.audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			SoundManager.instance.soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();
		}
		SaveLoadManager.instance.LoadVolumes ();
	}

	void OnLevelWasLoaded ()
	{
		SetAudioSources ();

		SoundManager.instance.soundSource.enabled = true;
		SoundManager.instance.soundSource.loop = false;
		SoundManager.instance.soundSource.playOnAwake = false;
		SoundManager.instance.audioSource.enabled = true;
		SoundManager.instance.audioSource.loop = true;
		SoundManager.instance.audioSource.playOnAwake = true;

		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			SoundManager.instance.audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			SoundManager.instance.soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();
		}
	}

	public void PlayFootstep ()
	{
		SetAudioSources ();
		SoundManager.instance.soundSource.loop = false;
		int choice = Random.Range (0, 1);
		switch (choice) {
		case 0:
			SoundManager.instance.soundSource.clip = walk1;
			break;
		case 1:
			SoundManager.instance.soundSource.clip = walk2;
			break;
		default:
			break;
		}
		if (!SoundManager.instance.soundSource.isPlaying) {
			SoundManager.instance.soundSource.Play ();
		}
	}

	public void PlayDeath ()
	{
		SetAudioSources ();
		SoundManager.instance.soundSource.loop = false;
		SoundManager.instance.soundSource.clip = death;
		if (!SoundManager.instance.soundSource.isPlaying)
			SoundManager.instance.soundSource.Play ();
	}

	public void PlayMine ()
	{	
		SetAudioSources ();
		SoundManager.instance.soundSource.loop = true;
		int choice = Random.Range (0, 2);
		switch (choice) {
		case 0:
			SoundManager.instance.soundSource.clip = mine4;
			break;
		case 1:
			SoundManager.instance.soundSource.clip = mine3;
			break;
		default:
			break;
		}
		if (!SoundManager.instance.soundSource.isPlaying) {
			SoundManager.instance.soundSource.Play ();
		}
	}

	public void StopLoop ()
	{
		SetAudioSources ();
		SoundManager.instance.soundSource.loop = false;
	}

	public void PlayMenu ()
	{
		SetAudioSources ();

		SoundManager.instance.soundSource.clip = menu;
		if (!SoundManager.instance.soundSource.isPlaying) {
			if (SoundManager.instance.soundSource.enabled)
				SoundManager.instance.soundSource.Play ();
		}
	}


	public void LoadOptions ()
	{
		SetAudioSources ();

		SoundManager.instance.audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
		SoundManager.instance.soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();

		SaveLoadManager.instance.LoadVolumes ();
		SoundManager.instance.soundSource.volume = SoundManager.instance.soundVolume;
		SoundManager.instance.soundSlider.value = SoundManager.instance.soundVolume;
		SoundManager.instance.audioSource.volume = SoundManager.instance.audioVolume;
		SoundManager.instance.audioSlider.value = SoundManager.instance.audioVolume;
	}

	public void SaveOptions ()
	{
		SetAudioSources ();
		float sound = SoundManager.instance.soundSlider.value;
		SoundManager.instance.soundSource.volume = sound;
		SoundManager.instance.soundVolume = sound;

		SoundManager.instance.soundSource.volume = SoundManager.instance.soundSlider.value;
		SoundManager.instance.audioSource.volume = SoundManager.instance.audioSlider.value;

		SoundManager.instance.soundVolume = SoundManager.instance.soundSource.volume;
		SoundManager.instance.audioVolume = SoundManager.instance.audioSource.volume;
		SaveLoadManager.instance.SaveVolumes ();
	}

	// Unity loses track of the audioSources upon loading new scenes
	private void SetAudioSources ()
	{
		SoundManager.instance.sources = SoundManager.instance.gameObject.GetComponentsInChildren<AudioSource> ();
		SoundManager.instance.audioSource = SoundManager.instance.sources [0];
		SoundManager.instance.soundSource = SoundManager.instance.sources [1];
	}


}
