using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	
	private const float OBSTACLE_SPEED = 5f; // Sets speed of obstacles moving towards player
	private const float OBSTACLE_DESTROY_POSITION = -15f; // x Position past player where Obstacles get destroyed and score increases
	private const float ENEMY_START_POSITION = 8f;
	private const int JUMP_OBSTACLE = 1;
	private const int DIVE_OBSTACLE = 2;
	private const int DUCK_OBSTACLE = 3;
	
	private List<Obstacle> obstacleList;
	
	private void Awake() {
		obstacleList = new List<Obstacle>();
	}
	
	private void Start() {
		spawnObstacle(JUMP_OBSTACLE, ENEMY_START_POSITION);
	}

	private void Update() {
		ObstacleMovement();

	}
	
	private void ObstacleMovement() {
		for (int i = 0; i < obstacleList.Count; i++) {
			Obstacle obstacle = obstacleList[i];
			obstacle.move();
	
			// Destory obstacles if player dodges them and they move too far to the left
			if (obstacle.getXPos() < OBSTACLE_DESTROY_POSITION) {
				// Destroy Obstacle
				obstacle.selfDestruct();
				Debug.Log("Obstacle Destroyed");
				
				// Remove object from list and decrement index so that we do not skip
				// any obstacles in the list above when one gets destroyed
				obstacleList.Remove(obstacle);
				i--;

				// if obstacle is destroyed, spawn a new one
				spawnObstacle(JUMP_OBSTACLE, ENEMY_START_POSITION);


				// Temp score track and printer
				GameAssets.GetInstance().increaseScore();
			}
		}
	}

	// This method spawns an obstacle
	private void spawnObstacle(float obstacleType, float xPos) {
		
		// Obstacles to jump over
		if (obstacleType == 1) {
			Transform jumpObstacle = Instantiate(GameAssets.GetInstance().jumpObsBody);
			jumpObstacle.position = new Vector3(xPos, -3.6f); // Initial position for obstacle
			obstacleList.Add(new Obstacle(jumpObstacle));
		}
		
		// Obstacles to dive through
		if (obstacleType == 2) {
			;
		}
		
		// Obstacles to duck under
		if (obstacleType == 3) {
			;
		}
	}
	
	// Class to store each obstacle on the screen 
	private class Obstacle {
		
		private Transform obstacle;
		
		public Obstacle(Transform t) {
			this.obstacle = t;
		}
		
		public void move() {
			obstacle.position += new Vector3(-1, 0, 0) * OBSTACLE_SPEED * Time.deltaTime;
		}
		
		public float getXPos() {
			return obstacle.position.x;
		}
		
		public void selfDestruct() {
			Destroy(obstacle.gameObject);
		}
	}
	
}


