using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

	public GameObject player;
	private GameObject mainCamera;
	private CameraController cameraController;



	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min; 
			maximum = max;
		}
	}

	public int columns = 32;
	public int rows = 32;
	private Count resourceCount = new Count (5, 15);
	public GameObject[] floorTiles;
	public GameObject[] resourceTiles;
	public GameObject outerWallTile;

	//to child child the generated prefabs
	private Transform boardHolder;

	private List <Vector3> gridPositions = new List<Vector3> ();

	//Initialise the grid
	private void InitialiseList ()
	{
		gridPositions.Clear ();
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				if (y == 0) //skip first level for player to move
					continue;
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	private void BoardSetup ()
	{
		//instantiate new GameObject and set the transform
		boardHolder = new GameObject ("Board").transform;
		//setup outer wall around the grid
		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				//first row to be emptied so that the player can be instantiated there
				if (y == rows - 1 && (x != -1) && x != columns) {
					continue;
				}

				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if (gridPositions.Contains (new Vector3 (x, y, 0f))) {
					//instantiate a gameobject with chosen prefab
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
					//set the parent to the boardholder
					instance.transform.SetParent (boardHolder);
					gridPositions.Remove (new Vector3 (x, y, 0f));
				}

				// outer wall needs to be created
				if (x == -1 || x == columns || y == rows) { // y==-1 for the lower wall
					toInstantiate = outerWallTile;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
					instance.transform.SetParent (boardHolder);
				}
			}
		}

	}

	//returns a random Vector from the grid and removes it from the list of available positions
	private Vector3 RandomVector ()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPos = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPos;
	}

	private void InstantiateAtRandom (GameObject[] array, int min, int max)
	{
		int count = Random.Range (min, max);
		for (int i = 0; i < count; i++) {
			Vector3 randomPos = RandomVector ();
			GameObject choice = array [Random.Range (0, array.Length)];
			GameObject instance = Instantiate (choice, randomPos, Quaternion.identity);
			instance.transform.SetParent (boardHolder);
		}
	}

	public void Initialise ()
	{
		InitialiseList ();
		InstantiateAtRandom (resourceTiles, resourceCount.minimum, resourceCount.maximum);
		BoardSetup ();
		//instantiate player at the top
		Vector3 newLocation = new Vector3 (Random.Range (0, columns - 1), rows - 1, 0f);
		Instantiate (player, newLocation, Quaternion.identity);
		//find camera and position to player - set offset correctly
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		newLocation.z = -10;
		mainCamera.transform.position = newLocation;
		cameraController = mainCamera.GetComponent<CameraController> ();
		cameraController.FindPlayer ();

	}
}
