using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public GameObject player; // For enemy to reference the Player; to check if player is on the left or right side

    public float within_range;

    public float speed;

    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= within_range) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        } 

        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x){ // check if player's x position is greater than enemy's (aka; if player is on right side)
            scale.x = Mathf.Abs(scale.x); //if yes, then enemy should face the left

        } else {
            scale.x = Mathf.Abs(scale.x) * -1; //otherwise enemy should face right

        }

        transform.localScale = scale;
    }

}
