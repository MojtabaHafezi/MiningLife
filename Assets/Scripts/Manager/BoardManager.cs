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
	//Dirt tile objects
	public GameObject[] floorTiles;
	//Stone tiles + lava
	public GameObject[] greyStoneTiles;
	public GameObject[] redStoneTiles;
	//resources and their corresponding occurence rate
	public GameObject[] coalTiles;
	//Resources and their corresponding amount to spawn for each instance
	private Count coalCount = new Count (5, 10);
	public GameObject[] ironTiles;
	private Count ironCount = new Count (4, 8);
	public GameObject[] silverTiles;
	private Count silverCount = new Count (2, 6);
	public GameObject[] goldTiles;
	private Count goldCount = new Count (1, 4);
	public GameObject[] emeraldTiles;
	private Count emeraldCount = new Count (0, 3);
	public GameObject[] rubyTiles;
	private Count rubyCount = new Count (0, 2);
	public GameObject[] diamondTiles;
	private Count diamondCount = new Count (0, 1);






	public GameObject outerWallTile;

	//to child child the generated prefabs
	private Transform boardHolder;

	private List <Vector3> gridPositions = new List<Vector3> ();
	private List<GameObject> objects = new List<GameObject> ();

	//Initialise the grid
	private void InitialiseList ()
	{
		rows = 10;
		//instantiate new GameObject and set the transform
		boardHolder = new GameObject (CONSTANTS.BOARD).transform;
		//Clear the lists
		gridPositions.Clear ();
		objects.Clear ();
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
				if (x == -1 || x == columns || y == rows) {
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
		InstantiateRandomResources ();
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

	// Create infinite level
	public void AddToList ()
	{
		gridPositions.Clear ();
		int beginRow = rows - 1;
		rows -= CONSTANTS.TILESTOSPAWN;

		for (int x = 0; x < columns; x++) {
			for (int y = beginRow; y >= rows; y--) {
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}

		//Each resource has its own occurence rate and if you go deeper -> higher probability
		InstantiateRandomResources ();
		//add the tiles to the board for all the empty positions
		TileSetUp (beginRow, rows);
		//remove old objects from scene as quick as new ones appear
		DestroyOldObjects ();

	}

	//Checks if occurence is available and instantiates the resources with the current random count
	private void InstantiateRandomResources ()
	{
		float depth = rows;
		float bonus = (float)(Mathf.Abs (depth) / 10f);

		//generate resources at random locations on the grid and remove available index from the list
		InstantiateAtRandom (coalTiles, coalCount.minimum, coalCount.maximum);

		//Iron: 75% 
		if (ThrowDice (75, 2 * bonus)) {
			InstantiateAtRandom (ironTiles, ironCount.minimum, ironCount.maximum);
		}

		//Silver: 45% 
		if (ThrowDice (45, 2 * bonus)) {
			InstantiateAtRandom (silverTiles, silverCount.minimum, silverCount.maximum);
		}

		//Gold: 15% 
		if (ThrowDice (15, 2 * bonus)) {
			InstantiateAtRandom (goldTiles, goldCount.minimum, goldCount.maximum);
		}

		//Emerald: 5% 
		if (ThrowDiceValuable (5, bonus)) {
			InstantiateAtRandom (emeraldTiles, emeraldCount.minimum, emeraldCount.maximum);
		}

		//Ruby: 2.5% 
		if (ThrowDiceValuable (2.5f, bonus)) {
			InstantiateAtRandom (rubyTiles, rubyCount.minimum, rubyCount.maximum);
		}

		//Diamond: 0.5% 
		if (ThrowDiceValuable (0.5f, bonus)) {
			InstantiateAtRandom (diamondTiles, diamondCount.minimum, diamondCount.maximum);
		}
	}

	//helper method - like a dice throw -takes in limit and a bonus from depth of dungeon
	private Boolean ThrowDiceValuable (float limit, float bonus)
	{
		float random = Random.Range (0f, 100f);
		if (random <= (limit + bonus))
			return true;

		return false;
	}
	//the valuable method is meant for the gems etc. -> half bonus only
	private Boolean ThrowDice (float limit, float bonus)
	{
		float random = Random.Range (0f, 100f);
		if (random <= (limit + bonus))
			return true;

		return false;
	}

	//set up for the tiles after initialisation
	//when the player moves downward -> new tiles will be generated to the bottom -> infinity
	private void TileSetUp (int lowerLimit, int higherLimit)
	{
		//setup outer wall around the grid
		for (int x = -1; x < columns + 1; x++) {
			for (int y = lowerLimit; y >= higherLimit; y--) {
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


	//destroy old objects the player wont see anymore
	private void DestroyOldObjects ()
	{
		if (rows < -15) {

			for (int i = 0; i < 156; i++) {
				Destroy (objects [i]);
				objects.Remove (objects [i]);
			}


		}
	}
}
