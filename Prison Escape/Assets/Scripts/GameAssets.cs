using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// House assets
// Obstacles, etc.

public class GameAssets : MonoBehaviour {
	
	private int score = 0;
    public Text txt;
    public Text health;

    private int Alive=3;

    public static GameAssets instance;
	
	public static GameAssets GetInstance() {
		return instance;
	}

	public void Awake() {
		instance = this;
	}

    public void Start() {
        health.text = "Health: " + Alive;
    }
	
	// Stores assets for obstacles that 
	public Transform jumpObsBody;
	public Transform duckObsBody;
	public Transform diveObsBody;
    public Transform bgBody;
    public Transform mgBody;
    public Transform fgBody;
    public Transform newGroundBody;

    public void increaseScore() {
		score++;
		Debug.Log("Current Score: " + score);
        txt.text = "Current Score: " + score;

    }
	
	public int getScore() {
		return score;
	}
	
	public void resetScore() {
		score = 0;
        
		Debug.Log("Current Score: " + score);
        txt.text = "Current Score: " + score;
       
    }
    public int reducehealth()
    {
        Alive -= 1;
        health.text = "Health: " + Alive;
        return Alive;

    }
}
