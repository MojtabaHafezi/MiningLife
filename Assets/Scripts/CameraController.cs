using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make the camera follow the player
public class CameraController : MonoBehaviour
{
	public GameObject gameManager;
	public GameObject soundManager;
	private GameObject player;
	//difference between camera and player transform positions
	private Vector3 offset;

	public 


	void Awake ()
	{
		
		player = GameObject.FindGameObjectWithTag ("Player");

		if (GameManager.instance == null)
			Instantiate (gameManager);
		if (SoundManager.instance == null)
			Instantiate (soundManager);
	}

	void Start ()
	{
		offset = transform.position - player.transform.position;
	}

	//guaranteed after all items have been processed in update -> player has already moved
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
