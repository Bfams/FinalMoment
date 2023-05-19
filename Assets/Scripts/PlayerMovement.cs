using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Variables
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    float horizontalInput;

    public float jumpForce = 5f;
    public bool isGrounded;
    public bool canDash;
    public float dashCooldownTime;

    public Animator animator;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Manage movement input and change sprite direction
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
            sr.flipX = false;

        if(horizontalInput < 0)
            sr.flipX = true;

        animator.SetFloat("Movement", horizontalInput);
        animator.SetBool("IsGrounded", isGrounded);

        //Jump if possible
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }


        //Dash if possible
        if(Input.GetKeyDown(KeyCode.E) && canDash)
        {
            if(sr.flipX)
            {
                rb.AddForce(Vector2.left * moveSpeed * 1.5f, ForceMode2D.Impulse);

            }
            else
            {
                rb.AddForce(Vector2.right * moveSpeed * 1.5f, ForceMode2D.Impulse);
            }
            canDash = false;
            animator.SetTrigger("Dash");
            
            if(isGrounded)
                StartCoroutine(DashCooldown());
        }
    }

    private void FixedUpdate()
    {
        //Move the player
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Allow the player to jump when they touch a game object tagged as ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isGrounded = true;
            canDash = true;
        }    
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }
}
