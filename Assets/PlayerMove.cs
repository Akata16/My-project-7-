using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    [SerializeField] private SpriteRenderer sr;

    public float jumpForce;

    public LayerMask whatGround;

    public Transform groundCheck;
    private bool doubleJump;

    private bool isGrounded;

    [SerializeField]private Animator anim;

    private enum Movementstate { idle,running,jumping,falling}
    private Movementstate state = Movementstate.idle;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        rb.velocity = new Vector2 (Input.GetAxisRaw("Horizontal") * moveSpeed,rb.velocity.y);


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatGround);
        

        


        if(isGrounded)
        {
            doubleJump = true;
        }
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.y, jumpForce);
                
            }
            else
            {

                if (doubleJump)
                {

                    rb.velocity = new Vector2(rb.velocity.y, jumpForce);
                    doubleJump = false;



                }



            }
        }
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        Movementstate state;
        if(rb.velocity.x > 0f)
        {
            state = Movementstate.running;
            sr.flipX = false;
        }
        else if(rb.velocity.x < 0f)
        {
            state = Movementstate.running;
            sr.flipX = true;
        }
        else
        {
            state = Movementstate.idle; 
        }
        if(rb.velocity.y > .1f)
        {
            state = Movementstate.jumping;
            
        }
        else if(rb.velocity.y < -.1f)
        {
            state = Movementstate.falling;
        }
        anim.SetInteger("state", (int)state);
    }
}
