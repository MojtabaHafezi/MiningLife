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



	// a count class for randomization, has a min and max value
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

	private int columns = 32;
	private int rows = 10;
	private Count coalCount = new Count (10, 20);
	public GameObject[] floorTiles;
	public GameObject[] coalTiles;
	public GameObject outerWallTile;

	//to child child the generated prefabs
	private Transform boardHolder;

	private List <Vector3> gridPositions = new List<Vector3> ();
	private List<GameObject> objects = new List<GameObject> ();

	//Initialise the grid
	private void InitialiseList ()
	{
		gridPositions.Clear ();
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				if (y == rows - 1) //skip first level for player to move
					continue;
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	private void BoardSetup ()
	{
		//instantiate new GameObject and set the transform
		boardHolder = new GameObject (CONSTANTS.BOARD).transform;
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
					objects.Add (instance);
					//set the parent to the boardholder
					instance.transform.SetParent (boardHolder);
					//remove from list of available gridpositions
					gridPositions.Remove (new Vector3 (x, y, 0f));
				}

				// outer wall needs to be created
				if (x == -1 || x == columns || y == rows) { // y==-1 for the lower wall
					toInstantiate = outerWallTile;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
					objects.Add (instance);
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

	//instantiate a random number between the range of given min and max
	//of objects at a random position that is still available
	private void InstantiateAtRandom (GameObject[] array, int min, int max)
	{
		int count = Random.Range (min, max);
		for (int i = 0; i < count; i++) {
			Vector3 randomPos = RandomVector ();
			GameObject choice = array [Random.Range (0, array.Length)];
			GameObject instance = Instantiate (choice, randomPos, Quaternion.identity);
			objects.Add (instance);
			instance.transform.SetParent (boardHolder);
		}
	}

	public void Initialise ()
	{
		//create a list to contain the vector3 positions of a grid
		InitialiseList ();
		//generate resources at random locations on the grid and remove available index from the list
		InstantiateAtRandom (coalTiles, coalCount.minimum, coalCount.maximum);
		//Rest of the grid is being instantiated with tiles or outer walls respectively
		BoardSetup ();
		//instantiate player at the top
		Vector3 newLocation = new Vector3 (Random.Range (0, columns - 1), rows - 1, 0f);
		Instantiate (player, newLocation, Quaternion.identity);
		//find camera and position to player - set offset correctly
		mainCamera = GameObject.FindGameObjectWithTag (CONSTANTS.MAINCAMERA);
		newLocation.z = -10;
		mainCamera.transform.position = newLocation;
		cameraController = mainCamera.GetComponent<CameraController> ();
		cameraController.FindPlayer ();
		rows = 0;
	}


	public void AddToList ()
	{
		int beginRow = rows;
		rows -= 4;

		for (int x = 0; x < columns; x++) {
			for (int y = beginRow; y > rows; y--) {
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}

		//TODO: Add random resources, depending on depth

		//add the tiles to the board for all the empty positions
		TileSetUp (beginRow, rows);


		DestroyOldObjects ();

	}
	//destroy old objects the player wont see anymore
	private void DestroyOldObjects ()
	{
		if (rows < -20) {

			for (int i = 0; i < 128; i++) {
				Destroy (objects [i]);
				objects.Remove (objects [i]);
			}


		}
	}

	//set up for the tiles after initialisation
	//when the player moves downward -> new tiles will be generated to the bottom -> infinity
	private void TileSetUp (int lowerLimit, int higherLimit)
	{
		//setup outer wall around the grid
		for (int x = -1; x < columns + 1; x++) {
			for (int y = lowerLimit; y > higherLimit; y--) {
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if (gridPositions.Contains (new Vector3 (x, y, 0f))) {
					//instantiate a gameobject with chosen prefab
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
					objects.Add (instance);
					//set the parent to the boardholder
					instance.transform.SetParent (boardHolder);
					//remove from list of available gridpositions
					gridPositions.Remove (new Vector3 (x, y, 0f));
				}

				// outer wall needs to be created on the SIDES only
				if (x == -1 || x == columns) { 
					toInstantiate = outerWallTile;
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
					instance.transform.SetParent (boardHolder);
					objects.Add (instance);
				}
			}
		}
	}
}
