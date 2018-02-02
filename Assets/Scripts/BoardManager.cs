using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
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

	public int columns = 8;
	public int rows = 8;
	public Count resourceCount = new Count (1, 5);
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
		for (int x = 1; x < columns - 1; x++) {
			for (int y = 1; y < rows - 1; y++) {
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
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if (x == -1 || x == columns || y == -1 || y == rows) {
					toInstantiate = outerWallTile;
				}
				//instantiate a gameobject with chosen prefab
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity);
				//set the parent to the boardholder
				instance.transform.SetParent (boardHolder);
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
		int count = 2;
		for (int i = 0; i < count; i++) {
			Vector3 randomPos = RandomVector ();
			GameObject choice = array [Random.Range (0, array.Length)];
			Instantiate (choice, randomPos, Quaternion.identity);
		}
	}

	public void Initialise ()
	{
		BoardSetup ();
		InitialiseList ();
		InstantiateAtRandom (resourceTiles, resourceCount.minimum, resourceCount.maximum);
	}
}
