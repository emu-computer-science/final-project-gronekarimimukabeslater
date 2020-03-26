using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	
	private const float OBSTACLE_SPEED = 3f;
	private const float OBSTACLE_DESTORY_POSITION = -15f;
	private List<Transform> obstacleList;
	private int score;
	
	private void Awake() {
		obstacleList = new List<Transform>();
	}
	
	private void Start() {
		spawnObstacle(0f, 8f);
	}

	private void Update() {
		ObstacleMovement();

	}
	
	private void ObstacleMovement() {
		for (int i = 0; i < obstacleList.Count; i++) {
			Debug.Log("Curious");
			if (obstacleList[i] != null) {
				obstacleList[i].position += new Vector3(-1, 0, 0) * OBSTACLE_SPEED * Time.deltaTime;
			} else
				continue;
			
			if (GetXPos(obstacleList[i]) < OBSTACLE_DESTORY_POSITION) {
				// Destroy Obstacle
				Destroy(obstacleList[i].gameObject);
				i--;
				Debug.Log("Obstacle Destroyed");
				//spawnObstacle(0f, 8f);
				score++;
				Debug.Log("Current score: " + score);
			}
		}
	}

	// This method spawns an obstacle
	private void spawnObstacle(float height, float xPos) {
	   Transform lowObs = Instantiate(GameAssets.GetInstance().lowObsBody);
	   lowObs.position = new Vector3(xPos, -3.6f); // Initial position for obstacle
	   obstacleList.Add(lowObs);
	}
	
	public float GetXPos(Transform t) {
		return t.position.x;
	}
	
}
