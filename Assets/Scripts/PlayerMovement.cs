using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer rbSpriteRenderer;
    private BoxCollider2D coll;
    private PlayerMovementInput moveInput;
    private Vector2 move;
/*    private float jumpTimeCounter;
*//*    private bool*//* isJumping;*/
    private enum MovementState {idle, running, jumping,falling }
    private float dirX = 0f;

    /*    [SerializeField]private float jumpTime;*/
    [SerializeField] private AudioSource jumpping;
    [SerializeField] private LayerMask jumpleGround;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 7f;
    [SerializeField] private FixedJoystick joystick;
    // Start is called before the first frame update

    private void Awake()
    {
        moveInput = new PlayerMovementInput();
    }

    private void OnEnable()
    {
        moveInput.Enable();
    }


    private void OnDisable()
    {
        moveInput.Disable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rbSpriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    private void Update()
    {
        move.x = joystick.Horizontal;
/*        rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);*/
        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);
        Debug.Log(move.x);
        if(moveInput.PlayerMoveInput.Jump.triggered && IsGrounded()) {
    /*      isJumping = true;
            jumpTimeCounter = jumpTime;*/
            jumpping.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }
/*        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = false;
        }*/
        RunningAnimation();
    }


    private void RunningAnimation()
    {
        MovementState state;
        if (move.x > 0)
        {
            rbSpriteRenderer.flipX = false;
            state = MovementState.running;
        }
        else if(move.x < 0)
        {
            rbSpriteRenderer.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        if( rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpleGround);
    }
}
