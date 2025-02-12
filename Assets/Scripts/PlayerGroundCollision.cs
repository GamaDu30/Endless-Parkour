using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{
    public bool onGround = false;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            onGround = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            onGround = true;
        }
    }
}
