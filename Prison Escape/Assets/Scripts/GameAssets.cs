using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// House assets
// Obstacles, etc.

public class GameAssets : MonoBehaviour {
	
	private int score = 0;
    public static GameAssets instance;
	
	public static GameAssets GetInstance() {
		return instance;
	}
	
	public void Awake() {
		instance = this;
	}
	
	// Stores assets for obstacles that 
	public Transform jumpObsBody;
	public Transform duckObsBody;
	
	public void increaseScore() {
		score++;
		Debug.Log("Current score: " + score);
	}
	
	public void resetScore() {
		score = 0;
		Debug.Log("Current score: " + score);
	}
}
