using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float walkSpeed;
    public float jumpImpulse;
    Vector2 moveInput; // pulls x and y movement (vector 2)
    TouchingDirections touchingDirections;

    bool isGrounded = true;

    public bool CanMove { get {
        return animator.GetBool(AnimationStrings.canMove);
    }}

    [SerializeField]
    private bool _isMoving = false;
    
    public bool IsMoving { get 
    {
        if(CanMove)
        {
            return _isMoving; 
        }
        else {
            return false;
        }

    } private set 
    {
        _isMoving = value;
        animator.SetBool(AnimationStrings.isMoving, value);
    } }

    public bool _isFacingRight = true; // default Player is facing right

    public bool IsFacingRight { get {
        return _isFacingRight;
    } private set{
        if (_isFacingRight != value) {
            // flip local scale to make player face opposite direction
            transform.localScale *= new Vector2(-1, 1); // leaving y scale alone but flipping x scale
        }
        _isFacingRight = value;
    } }

    private void Awake() { // if you want something found AS SOON AS POSSIBLE, use void Awake; it starts before the Start function
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>(); // x and y movement input

        IsMoving = moveInput != Vector2.zero; // checking to see if player is NOT moving in order to start moving = false vs true

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight) {
            // face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight){
            // face left
            IsFacingRight = false;
        }
    }

    // FINALLY - FIXED DOUBLE JUMP; THIS SECTION ONCOLLISIONENTER2D FIXES the one jump and then stop jumping bug; detects when player is on ground
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "floor" && isGrounded == false) {
            isGrounded = true;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if(isGrounded == true && context.started && touchingDirections && CanMove) {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        else {
            isGrounded = false;
        }

    }

    public void OnAttack(InputAction.CallbackContext context) {
        if(context.started) {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
