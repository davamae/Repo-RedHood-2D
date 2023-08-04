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
    Vector2 moveInput; // pulls x and y movement (vector 2)

    [SerializeField]
    private bool _isMoving = false;
    
    public bool IsMoving { get {
        return _isMoving;
    } private set {
        _isMoving = value;
        animator.SetBool("isMoving", value);
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
