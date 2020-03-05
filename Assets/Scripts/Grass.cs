using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public List<int> occupiedTiles;

    // Start is called before the first frame update
    void Start()
    {
        // Add 0-3 obstacles
        int count = Random.Range(0, 4); // 4 as the limit is exclusive
        for (int i = 0; i < count; i++)
        {
            // Pick a random position for the obstacle
            int pos = Random.Range(-4, 5);
            // Make sure the position isn't already occupied
            if (!occupiedTiles.Contains(pos))
            {
                // Pick either a tree or rock
                GameObject obstacle;
                if (Random.value < 0.7f)
                {
                    obstacle = treePrefab;
                } else
                {
                    obstacle = rockPrefab;
                }
                Instantiate(obstacle, new Vector3(pos, 0, transform.position.z), Quaternion.identity, transform);
                // Mark position as occupied
                occupiedTiles.Add(pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns true if a tile is occupied
    public bool IsTileOccupied(int position)
    {
        return occupiedTiles.Contains(position);
    }
}
