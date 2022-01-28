using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private Vector2 velocity;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        PollInput();
        rb.velocity = velocity;
    }

    /// <summary>
    /// Polls the input system for player input
    /// </summary>
    private void PollInput()
	{
        // Jump
        if(Input.GetKeyDown(KeyCode.W))
		{
            Jump();
		}

        // Move Left/Right or Stop
		if (Input.GetKey(KeyCode.A))
		{
            velocity.x = -moveSpeed;
		}
        else if (Input.GetKey(KeyCode.D))
		{
            velocity.x = moveSpeed;
        }
		else
		{
            velocity.x = 0;
        }
	}

    /// <summary>
    /// Make the player jump and don't allow them to jump until they've landed
    /// </summary>
    private void Jump()
	{
        if (!isJumping)
        {
            isJumping = true;
            velocity.y = jumpSpeed;
        }
	}

    /// <summary>
    /// Allow the player to jump once the trigger box collider collides with the ground
    /// </summary>
    /// <param name="other"></param>
	private void OnTriggerEnter2D(Collider2D other)
	{
        isJumping = false;
	}
}
