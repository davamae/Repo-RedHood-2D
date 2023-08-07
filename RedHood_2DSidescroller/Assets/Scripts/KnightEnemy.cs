using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public float walkSpeed = 0.5f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    public enum WalkableDirection {Right, Left}

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector;

    public WalkableDirection WalkDirection {
        get { return _walkDirection; }
        set { 
            if(_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                // check walkable direction specifically
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate() {
        if (touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } 
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else{
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }
}
