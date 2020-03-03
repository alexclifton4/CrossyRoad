using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        position = (int)transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
