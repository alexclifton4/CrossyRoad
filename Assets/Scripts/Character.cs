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
            if (ShouldMoveForward())
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
            else if (ShouldMoveLeft())
            {
                moving = true;
                startPos = transform.position;
                targetPos = startPos + Vector3.left;
                movementStartTime = Time.time;

                // Add vertical velocity so that it jumps
                rb.velocity = new Vector3(0, jumpVelocity, 0);
            }
            // Check for key presses to move right
            else if (ShouldMoveRight())
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

    // Called when the character is hit by a car
    private void OnTriggerEnter(Collider other)
    {
        if (enabled)
        {
            // Tell the game manager to stop the game
            gameManager.StopPlaying();

            // Tell the car that is has been hit
            other.gameObject.GetComponentInParent<Vehicle>().Stop();
        }
    }

    // Checks if the character should move forwards
    private bool ShouldMoveForward()
    {
        bool shouldMove = false;
        // Check for input
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Make sure the tile to the left isn't occupied
            Grass grassTile = gameManager.mapTiles[gameManager.score + 1].GetComponent<Grass>();
            if (!grassTile || (grassTile && !grassTile.IsTileOccupied((int)transform.position.x)))
            {
                shouldMove = true;
            }
        }
        return shouldMove;
    }

    // Checks if the character should move to the left
    private bool ShouldMoveLeft()
    {
        bool shouldMove = false;
        // Check for input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // Make sure its not at the edge of the map
            if (transform.position.x > -4)
            {
                // Make sure the tile to the left isn't occupied
                Grass grassTile = gameManager.mapTiles[gameManager.score].GetComponent<Grass>();
                if (!grassTile || (grassTile && !grassTile.IsTileOccupied((int)transform.position.x - 1)))
                {
                    shouldMove = true;
                }
            }
        }
        return shouldMove;
    }

    // Checks if the character should move to the right
    private bool ShouldMoveRight()
    {
        bool shouldMove = false;
        // Check for input
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // Make sure its not at the edge of the map
            if (transform.position.x < 4)
            {
                // Make sure the tile to the right isn't occupied
                Grass grassTile = gameManager.mapTiles[gameManager.score].GetComponent<Grass>();
                if (!grassTile || (grassTile && !grassTile.IsTileOccupied((int)transform.position.x + 1)))
                {
                    shouldMove = true;
                }
            }
        }
        return shouldMove;
    }
}
