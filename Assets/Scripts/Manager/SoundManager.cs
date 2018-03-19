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

		soundSource.enabled = true;
		soundSource.loop = false;
		soundSource.playOnAwake = false;
		audioSource.enabled = true;
		audioSource.loop = true;
		audioSource.playOnAwake = true;
		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();

		}
		SaveLoadManager.instance.LoadVolumes ();
	}

	void OnLevelWasLoaded ()
	{
		SetAudioSources ();

		soundSource.enabled = true;
		soundSource.loop = false;
		soundSource.playOnAwake = false;
		audioSource.enabled = true;
		audioSource.loop = true;
		audioSource.playOnAwake = true;

		if (SceneManager.GetActiveScene ().name == CONSTANTS.STARTSCENE) {
			audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
			soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();
		}
	}

	public void PlayFootstep ()
	{
		SetAudioSources ();
		soundSource.loop = false;
		int choice = Random.Range (0, 1);
		switch (choice) {
		case 0:
			soundSource.clip = walk1;
			break;
		case 1:
			soundSource.clip = walk2;
			break;
		default:
			break;
		}
		if (!soundSource.isPlaying) {
			soundSource.Play ();
		}
	}

	public void PlayDeath ()
	{
		SetAudioSources ();
		soundSource.loop = false;
		soundSource.clip = death;
		if (!soundSource.isPlaying)
			soundSource.Play ();
	}

	public void PlayMine ()
	{	
		SetAudioSources ();
		soundSource.loop = true;
		int choice = Random.Range (0, 2);
		switch (choice) {
		case 0:
			soundSource.clip = mine4;
			break;
		case 1:
			soundSource.clip = mine3;
			break;
		default:
			break;
		}
		if (!soundSource.isPlaying) {
			soundSource.Play ();
		}
	}

	public void StopLoop ()
	{
		SetAudioSources ();
		soundSource.loop = false;
	}

	public void PlayMenu ()
	{
		SetAudioSources ();

		soundSource.clip = menu;
		if (!soundSource.isPlaying) {
			if (soundSource.enabled)
				soundSource.Play ();
		}
	}


	public void LoadOptions ()
	{
		SetAudioSources ();

		audioSlider = GameObject.FindGameObjectWithTag (CONSTANTS.AUDIO).GetComponent<Slider> ();
		soundSlider = GameObject.FindGameObjectWithTag (CONSTANTS.SOUND).GetComponent<Slider> ();

		SaveLoadManager.instance.LoadVolumes ();
		soundSource.volume = soundVolume;
		soundSlider.value = soundVolume;
		audioSource.volume = audioVolume;
		audioSlider.value = audioVolume;
	}

	public void SaveOptions ()
	{
		SetAudioSources ();
		float sound = soundSlider.value;
		soundSource.volume = sound;
		soundVolume = sound;

		soundSource.volume = soundSlider.value;
		audioSource.volume = audioSlider.value;

		soundVolume = soundSource.volume;
		audioVolume = audioSource.volume;
		SaveLoadManager.instance.SaveVolumes ();

		LoadOptions ();
	}

	// Unity loses track of the audioSources upon loading new scenes
	private void SetAudioSources ()
	{
		sources = GetComponentsInChildren<AudioSource> ();
		audioSource = sources [0];
		soundSource = sources [1];
	}


}
