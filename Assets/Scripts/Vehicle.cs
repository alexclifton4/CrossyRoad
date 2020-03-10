using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed;
    public float loopPosition;
    public int direction;
    public float bounceHeight;
    public int bounceSpeed;

    private bool shouldStop = false;

    // Start is called before the first frame update
    void Start()
    {
        // Rotate the game object if direction is negative
        if (direction == -1)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the car across the road
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        // If the car is past its loop round position, go back to the start
        if (!shouldStop)
        {
            if ((direction == 1 && transform.position.x > loopPosition) || (direction == -1 && transform.position.x < loopPosition))
            {
                transform.position = new Vector3(-6 * direction, transform.position.y, transform.position.z);
            }
        } else
        {
            // Car has hit the player, so slow down
            speed /= (1 + Time.deltaTime);
            if (Mathf.Abs(speed) < 1) speed = 0;
        }

        // Bounce the car
        float y = bounceHeight * Mathf.Sin(bounceSpeed * speed * Time.timeSinceLevelLoad) + bounceHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    // Called by the character when it hits the car
    public void Stop()
    {
        shouldStop = true;
    }
}
