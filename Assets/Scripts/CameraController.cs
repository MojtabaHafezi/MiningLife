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

	void Start ()
	{
		if (GameManager.instance == null)
			Instantiate (gameManager);
		if (SoundManager.instance == null)
			Instantiate (soundManager);
		
	}

	//guaranteed after all items have been processed in update -> player has already moved
	void LateUpdate ()
	{
		if (player != null)
			transform.position = player.transform.position + offset;
	}

	public void FindPlayer ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null)
			offset = transform.position - player.transform.position;
	}
}
