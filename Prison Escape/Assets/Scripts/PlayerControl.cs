using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    public float jumpVelocity = 8f;
    public float fallMultipler = 2.5f;

    private Rigidbody2D body;
    private bool isJumping;
    private bool isDucking;
    private bool isDiving;
    private BoxCollider2D playerCollider;
    float mScaleX, mScaleY;

    void Awake() {
        body = player.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        mScaleY = playerCollider.size.y;
        mScaleX = playerCollider.size.x;
    }

    void Update() {
        // Basic Jumping Motion
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping) {
            body.velocity += Vector2.up * jumpVelocity;
            isJumping = true;
            isDiving = true; //Prevents the player from diving in mid air
        }
        
        // Only Allow 1 jump at a time (No Infinite Jumps)
        if (body.velocity.y == 0) {
            isJumping = false;
        }

        // Improved Falling / Jumping
        if (body.velocity.y < 0) { // Falling
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler - 1) * Time.deltaTime;
        } else if (body.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow)) { // Long / Hold Jump
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler - 1) * Time.deltaTime;
        }

        //Ducking
        if(Input.GetKeyDown(KeyCode.DownArrow) && !isDucking)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2);
            isDucking = true;
        }

        //Allow one duck at a time
        if(playerCollider.size.y == mScaleY)
        {
            isDucking = false;
        }

        //Get up from ducking
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 2);
        }


        //Diving
        if(Input.GetKeyDown(KeyCode.RightArrow) && !isDiving)
        {
            body.velocity += Vector2.up * (jumpVelocity);
            playerCollider.size = new Vector2(playerCollider.size.x * 2, playerCollider.size.y / 2);
            body.transform.localScale =  new Vector2(body.transform.localScale.x * 2, body.transform.localScale.y /2); //This will be removed once we have player sprites
            isDiving = true;
            isJumping = true;
        }

        if(playerCollider.size.x == mScaleX && body.velocity.y == 0)
        {
            isJumping = false;
            isDiving = false;
        }

        if(isDiving == true && body.velocity.y == 0)
        {
            playerCollider.size = new Vector2(playerCollider.size.x / 2, playerCollider.size.y * 2);
            body.transform.localScale = new Vector2(body.transform.localScale.x / 2, body.transform.localScale.y * 2); //This will be removed once we have player sprites
            isDucking = false;
        }
    }
}
