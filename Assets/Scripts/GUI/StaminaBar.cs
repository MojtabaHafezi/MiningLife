using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

	private GameManager gameManager;
	private Slider staminaSlider;
	// Use this for initialization
	void Start ()
	{
		gameManager = GameManager.instance;
		staminaSlider = GetComponent<Slider> ();
		staminaSlider.maxValue = gameManager.stamina;
		staminaSlider.minValue = 0;
		staminaSlider.value = staminaSlider.maxValue;

	}

	void Update ()
	{
		staminaSlider.value = gameManager.stamina;
	}
}
