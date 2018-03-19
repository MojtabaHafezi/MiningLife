using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SaveLoadManager : MonoBehaviour
{

	//Singleton pattern
	public static SaveLoadManager instance = null;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

	}


	public void SetDefaultData ()
	{
		
		GameManager.instance.inventory.SetDefaultData ();
		PlayerPrefs.SetInt (CONSTANTS.STAMINA, 100);
		PlayerPrefs.SetInt (CONSTANTS.CURRENCY, 50);
		PlayerPrefs.SetInt (CONSTANTS.EFFICIENCY, 1);
		PlayerPrefs.Save ();
	}

	public void SaveAllData ()
	{
		
		
		GameManager.instance.inventory.SaveData ();
		PlayerPrefs.SetInt (CONSTANTS.STAMINA, GameManager.instance.stamina);
		PlayerPrefs.SetInt (CONSTANTS.CURRENCY, GameManager.instance.currency);
		PlayerPrefs.SetInt (CONSTANTS.EFFICIENCY, GameManager.instance.efficiency);
	

		PlayerPrefs.Save ();
	}

	public void LoadAllData ()
	{
		
		GameManager.instance.inventory.LoadData ();
		GameManager.instance.stamina = PlayerPrefs.GetInt (CONSTANTS.STAMINA, 100);
		GameManager.instance.currency = PlayerPrefs.GetInt (CONSTANTS.CURRENCY, 50);
		GameManager.instance.efficiency = PlayerPrefs.GetInt (CONSTANTS.EFFICIENCY, 1);

	}

	public void SaveVolumes ()
	{
		PlayerPrefs.SetFloat (CONSTANTS.AUDIO, SoundManager.instance.audioVolume);
		PlayerPrefs.SetFloat (CONSTANTS.SOUND, SoundManager.instance.soundVolume);
		PlayerPrefs.Save ();

	}

	public void LoadVolumes ()
	{
		SoundManager.instance.audioVolume = PlayerPrefs.GetFloat (CONSTANTS.AUDIO, 0.15f);
		SoundManager.instance.soundVolume = PlayerPrefs.GetFloat (CONSTANTS.SOUND, 0.1f);
	}

}
