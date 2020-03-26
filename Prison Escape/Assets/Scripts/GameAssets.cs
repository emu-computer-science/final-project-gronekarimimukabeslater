using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// House assets
// Obstacles, etc.

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;
	
	public static GameAssets GetInstance() {
		return instance;
	}
	
	public void Awake() {
		instance = this;
	}
	
	public Sprite lowObstacle;
	public Transform lowObsBody;
}
