using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Fields

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PollInput();
    }

    private void PollInput()
	{
        if(Input.GetKeyDown(KeyCode.W))
		{
            // Jump
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
            // Move Left
		}
        else if (Input.GetKeyDown(KeyCode.D))
		{
            // Move Right
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
            // Toggle Light/Dark Mode
		}
	}
}
