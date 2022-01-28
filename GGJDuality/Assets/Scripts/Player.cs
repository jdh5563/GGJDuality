using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    // Player rigidbody
    [SerializeField] private Rigidbody2D rb;

    // Player physics modifiers
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    private Vector2 velocity;
    private bool isJumping = false;

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        PollInput();
        //rb.velocity = velocity;
    }

	private void FixedUpdate()
	{
		rb.velocity = velocity;
	}

	/// <summary>
	/// Polls the input system for player movement input
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
	private void OnTriggerEnter2D(Collider2D other)
	{
        isJumping = false;
	}

    /// <summary>
    /// While the player is colliding with the tilemap, check if they are adjacent to the flag
    /// If they are, they win the level
    /// </summary>
	private void OnCollisionStay2D(Collision2D collision)
	{
        // Iterate through the 8 surrounding tiles
		Tilemap tilemap = collision.collider.GetComponent<Tilemap>();
        for(int i = -1; i < 2; i++)
		{
            for(int j = -1; j < 2; j++)
			{
                // Get the tile at the corresponding position relative to the player
                Vector3 surroundingTilePosition = new Vector3(transform.position.x + i, transform.position.y + j);
                Tile tile = tilemap.GetTile<Tile>(tilemap.WorldToCell(surroundingTilePosition));

                // The player wins if the "Flag" tile is adjacent
                if (tile != null && tile.name == "Flag")
                {
                    Debug.Log("Player wins!");
                }
            }
        }
	}
}
