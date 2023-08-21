using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public GameObject player; // For enemy to reference the Player; to check if player is on the left or right side

    public bool flip; // for more than one enemy, so all enemies will flip to face Player

    public float speed;

    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x){ // check if player's x position is greater than enemy's (aka; if player is on right side)
            scale.x = Mathf.Abs(scale.x); //if yes, then enemy should face the left

        } else {
            scale.x = Mathf.Abs(scale.x) * -1; //otherwise enemy should face right

        }

        transform.localScale = scale;
    }

}
