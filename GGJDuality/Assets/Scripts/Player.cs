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

    private void PollInput()
	{
        if(Input.GetKeyDown(KeyCode.W))
		{
            Jump();
		}

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

    private void Jump()
	{
        if (!isJumping)
        {
            isJumping = true;
            velocity.y = jumpSpeed;
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        ContactPoint2D contact = collision.GetContact(0);

        // TODO: Make sure the player makes contact with the TOP of the platform
		//if(contact.point.x > transform.position.x + transform.localScale.x / 2 && contact.point.x < transform.position.x - transform.localScale.x / 2)
		//{
  //          isJumping = false;
		//}
	}
}
