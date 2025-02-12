using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CamUpdate : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 offset;
    Transform player;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        offset = transform.position - player.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * speed);
    }
}
