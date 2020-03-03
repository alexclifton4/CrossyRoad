using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float speed;
    public float loopPosition;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the car across the road
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        // If the car is past its loop round position, go back to the start
        if ((direction == 1 && transform.position.x > loopPosition) || (direction == -1 && transform.position.x < loopPosition))
        {
            transform.position = new Vector3(-5 * direction, transform.position.y, transform.position.z);
        }
    }
}
