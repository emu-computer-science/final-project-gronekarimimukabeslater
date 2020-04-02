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
    private const float FOREGROUND_SPEED = 3f; //Sets the speed of the background elements
    private const float MIDGROUND_SPEED = 2f; //Sets the speed of the midground elements
    private const float BACKGROUND_SPEED = 1f; //Sets the speed of the background elements

    //Create two objects of each to act as a double buffer
    private Transform mg1Transform;
    private Transform mg2Transform;
    private Transform bg1Transform;
    private Transform bg2Transform;
    private Transform fg1Transform;
    private Transform fg2Transform;
    private Transform ngTransform;
    private Transform ng2Transform;

    private List<Obstacle> obstacleList;
	
	private void Awake() {
		obstacleList = new List<Obstacle>();
	}
	
	private void Start() {
		spawnObstacle(ENEMY_START_POSITION);
        CreateBackground();


    }

	private void Update() {
		ObstacleMovement();
        BackgroundMovement();
	}

    private void CreateBackground()
    {
        //Creates the background objects and positions them on the scene
        mg2Transform = Instantiate(GameAssets.GetInstance().mgBody);
        mg1Transform = Instantiate(GameAssets.GetInstance().mgBody);
        bg1Transform = Instantiate(GameAssets.GetInstance().bgBody);
        bg2Transform = Instantiate(GameAssets.GetInstance().bgBody);
        fg2Transform = Instantiate(GameAssets.GetInstance().fgBody);
        fg1Transform = Instantiate(GameAssets.GetInstance().fgBody);
        ngTransform = Instantiate(GameAssets.GetInstance().newGroundBody);
        ng2Transform = Instantiate(GameAssets.GetInstance().newGroundBody);

        mg2Transform.position = new Vector3(32.8f, 3.8f);
        mg1Transform.position = new Vector3(-0.4f, 3.8f);
        bg1Transform.position = new Vector3(5f, 3.5f);
        bg2Transform.position = new Vector3(39f, 3.5f);
        fg1Transform.position = new Vector3(2.6f, 0f, 30f);
        fg2Transform.position = new Vector3(23f, 0f, 30f);
        ngTransform.position = new Vector3(-0.24f, -5.72f, 3f);
        ng2Transform.position = new Vector3(18f, -5.72f, 3f);
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
				spawnObstacle(ENEMY_START_POSITION);
				
				// Temp score track and printer
				GameAssets.GetInstance().increaseScore();
			}
		}
	}

    private void BackgroundMovement()
    {
        mg1Transform.position += new Vector3(-1, 0, 0) * MIDGROUND_SPEED * Time.deltaTime;
        mg2Transform.position += new Vector3(-1, 0, 0) * MIDGROUND_SPEED * Time.deltaTime;
        bg1Transform.position += new Vector3(-1, 0, 0) * BACKGROUND_SPEED * Time.deltaTime;
        bg2Transform.position += new Vector3(-1, 0, 0) * BACKGROUND_SPEED * Time.deltaTime;
        fg1Transform.position += new Vector3(-1, 0, 0) * FOREGROUND_SPEED * Time.deltaTime;
        fg2Transform.position += new Vector3(-1, 0, 0) * FOREGROUND_SPEED * Time.deltaTime;
        ngTransform.position += new Vector3(-1, 0, 0) * OBSTACLE_SPEED * Time.deltaTime;
        ng2Transform.position += new Vector3(-1, 0, 0) * OBSTACLE_SPEED * Time.deltaTime;

        if (mg1Transform.position.x < -27f)
        {
            mg1Transform.position = new Vector2(32.8f, 3.8f);
        }
        if (mg2Transform.position.x < -27f)
        {
            mg2Transform.position = new Vector2(32.8f, 3.8f);
        }
        if (bg1Transform.position.x < -27f)
        {
            bg1Transform.position = new Vector2(39f, 3.5f);
        }
        if (bg2Transform.position.x < -27f)
        {
            bg2Transform.position = new Vector2(39f, 3.5f);
        }
        if (fg1Transform.position.x < -17.8f)
        {
            fg1Transform.position = new Vector3(23f, 0f, 30f);
        }
        if (fg2Transform.position.x < -17.8f)
        {
            fg2Transform.position = new Vector3(23f, 0f, 30f);
        }
        if (ngTransform.position.x < -21f)
        {
            ngTransform.position = new Vector3(16f, -5.72f, 3f);
        }
        if (ng2Transform.position.x < -21f)
        {
            ng2Transform.position = new Vector3(16f, -5.72f, 3f);
        }

    }

    // This method spawns an obstacle
    private void spawnObstacle(float xPos) {
		
		int obstacleType  = Random.Range(1, 4);

		// Obstacles to jump over
		if (obstacleType == 1) {
			Transform jumpObstacle = Instantiate(GameAssets.GetInstance().jumpObsBody);
			jumpObstacle.position = new Vector3(xPos, -3.5f); // Initial position for obstacle
			obstacleList.Add(new Obstacle(jumpObstacle));
		}
		
		// Obstacles to dive through
		if (obstacleType == 2) {
            Transform diveObstacle = Instantiate(GameAssets.GetInstance().diveObsBody);
            diveObstacle.position = new Vector3(xPos, -2.5f); // Initial position for obstacle
            obstacleList.Add(new Obstacle(diveObstacle));
		}
		
		// Obstacles to duck under
		if (obstacleType == 3) {
			Transform duckObstacle = Instantiate(GameAssets.GetInstance().duckObsBody);
			duckObstacle.position = new Vector3(xPos, -2.8f); // Initial position for obstacle
			obstacleList.Add(new Obstacle(duckObstacle));
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


