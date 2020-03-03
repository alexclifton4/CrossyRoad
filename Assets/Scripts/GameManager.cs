﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public Transform map;
    public GameObject grassPrefab;
    public GameObject roadPrefab;
    public int mapTilesToKeep;
    public int minimumTiles;

    public Dictionary<int, GameObject> mapTiles;

    private bool lastWasGrass = true;
    private int nextTileIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Create the initial map
        mapTiles = new Dictionary<int, GameObject>();
        // Instantiate some grass at the beginning 
        mapTiles.Add(0, Instantiate(grassPrefab, new Vector3(0, 0, 0), Quaternion.identity, map));
        mapTiles.Add(1, Instantiate(grassPrefab, new Vector3(0, 0, 1), Quaternion.identity, map));

        for (int i = 2; i < 12; i++)
        {
            mapTiles.Add(i, Instantiate(grassPrefab, new Vector3(0, 0, i), Quaternion.identity, map));
        }
        nextTileIndex = mapTiles.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // If there are not enough map tiles, add some more
        if (mapTiles.Count < minimumTiles)
        {
            // Alternate between grass and other elements
            if (lastWasGrass)
            {
                CreateRoad();
                lastWasGrass = false;
            }
            else
            {
                CreateGrass();
                lastWasGrass = true;
            }
        }
    }

    // Called when the character is moved forward
    public void moveForward()
    {
        // Increase the score
        score++;

        // Destroy the last tile
        if (score >= mapTilesToKeep)
        {
            Destroy(mapTiles[score - mapTilesToKeep]);
            mapTiles.Remove(score - mapTilesToKeep);
        }
    }

    // Creates a grass segment to add to the map
    private void CreateGrass()
    {
        // Generate either 1, 2 or 3 grass tiles
        int count = Random.Range(1, 4); // 4 because upper bound is exclusive
        for (int i = 0; i < count; i++)
        {
            // Create a road tile
            mapTiles.Add(nextTileIndex, Instantiate(grassPrefab, new Vector3(0, 0, nextTileIndex), Quaternion.identity, map));
            nextTileIndex++;
        }
    }

    // Creates a road segment to add to the map
    private void CreateRoad()
    {
        // Generate either 1, 2 or 3 road tiles
        int count = Random.Range(1, 4); // 4 because upper bound is exclusive
        for (int i = 0; i < count; i++)
        {
            // Create a road tile
            mapTiles.Add(nextTileIndex, Instantiate(roadPrefab, new Vector3(0, 0, nextTileIndex), Quaternion.identity, map));
            nextTileIndex++;
        }
    }
}