using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemy : MonoBehaviour
{
    public GameObject player; // For enemy to reference the Player; to check if player is on the left or right side
    public DetectionZone attackZone;
    Animator animator;

    public float within_range; // For enemy to check if Player is WITHIN range to chase

    public float speed; // Speed of Enemy

    Rigidbody2D rb;

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget;} private set {
        _hasTarget = value;
        animator.SetBool(AnimationStrings.hasTarget, value);
    } }

    // BENEATH IS CODE MEANT TO BE USED TO FREEZE ENEMY WHEN ATTACKING; NOT QUITE WORKING; DON'T NEED
    // public bool CanMove
    // {
    //     get {
    //         return animator.GetBool(AnimationStrings.canMove);
    //     }
    // }

    // public bool _isRunning = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {

        HasTarget = attackZone.detectedColliders.Count > 0;

        float dist = Vector3.Distance(player.transform.position, transform.position); // distance of the Player from the Enemy's position

        if (dist <= within_range) 
        
        { // If Player's distance is less or equal to being within the range to Chase
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // then Enemy's transform.position equals moving Towards player's position
        }




        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x){ // check if player's x position is greater than enemy's (aka; if player is on right side)
            scale.x = Mathf.Abs(scale.x); //if yes, then enemy should face the left
            animator.SetBool("isRunning", true);

        } else {
            scale.x = Mathf.Abs(scale.x) * -1; //otherwise enemy should face right

        }

        transform.localScale = scale;
    }

}
