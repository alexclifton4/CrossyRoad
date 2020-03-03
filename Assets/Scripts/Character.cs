using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float jumpVelocity;
    public float moveTime;

    public GameManager gameManager;

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float movementStartTime;
    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow movement if not already moving
        if (!moving)
        {
            // Check for key presses to move forward
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                moving = true;
                startPos = transform.position;
                targetPos = startPos + Vector3.forward;
                movementStartTime = Time.time;

                // Add vertical velocity so that it jumps
                rb.velocity = new Vector3(0, jumpVelocity, 0);

                // Tell the game manager that its moved forward
                gameManager.moveForward();
            }
            // Check for key presses to move left
            else if (transform.position.x > -4 && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
            {
                moving = true;
                startPos = transform.position;
                targetPos = startPos + Vector3.left;
                movementStartTime = Time.time;

                // Add vertical velocity so that it jumps
                rb.velocity = new Vector3(0, jumpVelocity, 0);
            }
            // Check for key presses to move right
            else if (transform.position.x < 4 && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
            {
                moving = true;
                startPos = transform.position;
                targetPos = startPos + Vector3.right;
                movementStartTime = Time.time;

                // Add vertical velocity so that it jumps
                rb.velocity = new Vector3(0, jumpVelocity, 0);
            }
        }

        // If moving, Lerp to the new position
        if (moving)
        {
            // Lerp will mess up the Y velocity, which should be independent
            // So remember it and set it after the Lerp
            float y = transform.position.y;

            float percentComplete = (Time.time - movementStartTime) / moveTime;
            transform.position = Vector3.Lerp(startPos, targetPos, percentComplete);

            // Reset the y position
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            // Stop moving if its there
            if (percentComplete > 1)
            {
                moving = false;
            }
        }
    }
}
