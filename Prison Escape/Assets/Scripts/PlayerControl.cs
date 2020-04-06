﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    public float jumpVelocity = 8f;
    public float fallMultipler = 2.5f;
    public float diveFloatTime = .5f;

    private Rigidbody2D body;
    private bool isJumping;
    private bool isDucking;
    private bool isDiving;
    private BoxCollider2D playerCollider;
    float mScaleX, mScaleY;

    private float startJump;
    private Animator playerAnimator;
    private int Alive;

    void Awake() {
        body = player.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        mScaleY = playerCollider.size.y;
        mScaleX = playerCollider.size.x;

        playerAnimator = GetComponent<Animator>();
    }

    void Update() {

        // Basic Jumping Motion
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !isJumping) {
            body.velocity += Vector2.up * jumpVelocity;
            isJumping = true;
            isDiving = true; //Prevents the player from diving in mid air
            playerAnimator.SetBool("Jump", true); //Plays jumping animation
        }
        
        // Only Allow 1 jump at a time (No Infinite Jumps)
        if (body.velocity.y == 0) {
            isJumping = false;
            playerAnimator.SetBool("Jump", false);
        }

        // Improved Falling / Jumping
        if (body.velocity.y < 0) { // Falling
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler - 1) * Time.deltaTime;
        } else if (body.velocity.y > 0 && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) { // Long / Hold Jump
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler - 1) * Time.deltaTime;
        } 

        //Ducking
        if(Input.GetKeyDown(KeyCode.DownArrow) && !isDucking || Input.GetKeyDown(KeyCode.S) && !isDucking)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2);
            isDucking = true;
            playerAnimator.SetBool("Ducking", true);
            playerAnimator.SetBool("Jump", false); //Stop jump animation if player ducks mid air
        }

        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("Ducking", false);
        }

        //Allow one duck at a time
        if(playerCollider.size.y == mScaleY)
        {
            isDucking = false;
        }

        //Get up from ducking
        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 2);
        }

        //Diving
        if(Input.GetKeyDown(KeyCode.RightArrow) && !isDiving || Input.GetKeyDown(KeyCode.D) && !isDiving)
        {
            body.velocity += Vector2.up * (jumpVelocity - 2f);
            playerCollider.size = new Vector2(playerCollider.size.x * 2, playerCollider.size.y / 2);
            //body.transform.localScale =  new Vector2(body.transform.localScale.x * 2, body.transform.localScale.y /2); //This will be removed once we have player sprites
            isDiving = true;
            isJumping = true;
            startJump = Time.realtimeSinceStartup;
            playerAnimator.SetBool("Landed", false);
            //playerAnimator.SetFloat("DiveHeight", body.velocity.y);
        }

        if(playerCollider.size.x == mScaleX && body.velocity.y == 0)
        {
            isJumping = false;
            isDiving = false;
            playerAnimator.SetBool("Landed", true);
        }

        if (isDiving && body.velocity.y < 0 && (startJump + diveFloatTime) > Time.realtimeSinceStartup) {
            body.velocity = new Vector2(0,.2f);
        }

        if(isDiving == true && body.velocity.y == 0)
        {
            playerCollider.size = new Vector2(playerCollider.size.x / 2, playerCollider.size.y * 2);
            //body.transform.localScale = new Vector2(body.transform.localScale.x / 2, body.transform.localScale.y * 2); //This will be removed once we have player sprites
            isDucking = false;
        }

    }
	
	private void OnTriggerEnter2D(Collider2D collider) {

        Alive= GameAssets.GetInstance().reducehealth();
        if (Alive == 0)
        {
            SceneManager.LoadScene("Main");
            GameAssets.GetInstance().resetScore();
        }
         collider.gameObject.transform.position=new Vector2(collider.gameObject.transform.position.x, -100f);
		
	}
}
