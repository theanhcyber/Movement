using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D bcol2D;

    [SerializeField] private LayerMask jumpableGround;

    //Variable for action
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSourceEffect;
    [SerializeField] private AudioSource runSourceEffect;


    //double jump
    private int jumpCount = 0;
    private int maxJump = 2;

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bcol2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(dirX * moveSpeed, rb2D.velocity.y);

        if(IsGrounded())
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpSourceEffect.Play();
            rb2D.velocity = new Vector3(rb2D.velocity.x, jumpForce);
            jumpCount++;
        }

        UpdateAnimatorStates();
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bcol2D.bounds.center, bcol2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void UpdateAnimatorStates()
    {
        MovementState state;

        if (dirX > 0f)
        {
            runSourceEffect.Play();
            spriteRenderer.flipX = false;
            state = MovementState.running;
        }
        else if(dirX < 0f)
        {
            runSourceEffect.Play();
            spriteRenderer.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb2D.velocity.y > 0)
        {
            state = MovementState.jumping;
        }else if(rb2D.velocity.y < 0)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int) state);
    }


}
