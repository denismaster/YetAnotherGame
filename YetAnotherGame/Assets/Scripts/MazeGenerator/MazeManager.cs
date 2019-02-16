using UnityEngine;
using System.Collections;

public class MazeManager : MonoBehaviour {

	public Maze mazePrefab;

	private Maze mazeInstance;


	private void Start () {
		BeginGame();
	}
	


	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		
		mazeInstance.Generate();
		// playerInstance = Instantiate(playerPrefab) as Player;
		// playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
	}

}