using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject player;
    public float jumpVelocity = 8f;
    public float fallMultipler = 2.5f;

    private Rigidbody2D body;
    private bool isJumping;

    void Awake() {
        body = player.GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Basic Jumping Motion
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping) {
            body.velocity += Vector2.up * jumpVelocity;
            isJumping = true;
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
    }
}
