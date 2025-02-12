using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] PlayerGroundCollision playerGroundDetection;
    [SerializeField] PlayerWallCollision playerWallDetection;

    Vector3 dest;

    Rigidbody rb;

    bool isJumping = false;
    bool isWallClimbing = false;
    bool hasWallJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Touch Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchScreenPosition = touch.position;

            touchScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition);

            dest.x = touchWorldPosition.x;
        }

        Vector3 velocity = rb.velocity;
        velocity.x = (dest.x - transform.position.x) * speed;
        rb.velocity = velocity;

        if (!playerGroundDetection.onGround)
        {
            if (!isJumping)
            {
                isJumping = true;
                rb.useGravity = true;
                rb.velocity += Vector3.up * jumpForce;
            }
        }
        else if (isJumping)
        {
            isJumping = false;
            rb.useGravity = false;
            hasWallJumped = false;
        }

        if (playerWallDetection.onWall)
        {
            if (!isWallClimbing)
            {
                isWallClimbing = true;
                rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
        }
        else if (isWallClimbing)
        {
            isWallClimbing = false;
            rb.useGravity = true;

            if (!hasWallJumped)
            {
                rb.velocity += Vector3.up * jumpForce * 0.5f;
                hasWallJumped = true;
            }
        }
    }
}
