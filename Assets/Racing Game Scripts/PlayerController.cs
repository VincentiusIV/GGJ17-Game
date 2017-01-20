using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;

    private Rigidbody playerRB;
    private Vector3 movement;
    private Vector3 jumping;
	// Called on startup
	void Start ()
    {
        playerRB = GetComponent<Rigidbody>();
	}
	
	// FixedUpdate is called every 0.02 seconds
	void Update ()
    {
        // Store input axis
        float jump = Input.GetAxis("Jump") * jumpSpeed; //* jumpSpeed * Time.deltaTime;
        float h = 1.0f * speed;

        Move(h, jump);        
    }

    void Move(float _h, float _jump)
    {
        // Set the movement vector based on the axis input.
        movement.Set(0f, _jump, _h);

        // Makes the movement vector proportional to the speed per second.
        movement *= Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRB.MovePosition(transform.position + movement);
    }
}
