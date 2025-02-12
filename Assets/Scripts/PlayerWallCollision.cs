using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallCollision : MonoBehaviour
{
    public bool onWall = false;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            onWall = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            onWall = true;
        }
    }
}
