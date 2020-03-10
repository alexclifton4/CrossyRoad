using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject lorryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate a vehicle
        GameObject vehicleObj;
        float startPos = Random.Range(-4f, 4);
        // Choose a vehicle type
        if (Random.value < 0.8)
        {
            // Car
            vehicleObj = Instantiate(carPrefab, transform);
        } else
        {
            // Lorry
            vehicleObj = Instantiate(lorryPrefab, transform);
        }
        vehicleObj.transform.position = new Vector3(startPos, 0, transform.position.z);
        Vehicle vehicle = vehicleObj.GetComponent<Vehicle>();
        vehicle.speed = Random.Range(4f, 8f);

        // Choose either left or right
        if (Random.value > 0.5)
        {
            // Right
            vehicle.direction = 1;
            vehicle.loopPosition = Random.Range(5.5f, 10f);
        } else
        {
            // Left
            vehicle.direction = -1;
            vehicle.loopPosition = Random.Range(-5.5f, -10f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
