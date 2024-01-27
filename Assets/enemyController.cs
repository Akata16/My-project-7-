using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private enum Movementstate { idle, running  }
    private Movementstate state = Movementstate.idle;
    public float moveSpeeds;

    public Transform leftpoint, rightpoint;

    private bool movingRight;

   

    

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
         
        anim = GetComponent<Animator>();

        leftpoint.parent = null;
        rightpoint.parent = null;
        // bunlar þu dememk bir objenin altýnda olsa bile baðýmsýz olsun demek yani child olmasýn demek
        movingRight = true;

        moveCount = moveTime;
    }


    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {

                rb.velocity = new Vector2(moveSpeeds, rb.velocity.y);

                sr.flipX = false;

                if (transform.position.x > rightpoint.position.x)
                {

                    movingRight = false;


                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeeds, rb.velocity.y);

                sr.flipX = true;

                if (transform.position.x < leftpoint.position.x)
                {
                    movingRight = true;


                }
            }
            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }
            anim.SetBool("isMoving", true);

        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = waitCount = Random.Range(moveTime * .75f, waitTime * .75f);
            }
            anim.SetBool("isMoving", false);
        }
        UpdateAnimationState();

    }











private void UpdateAnimationState()
    {
        Movementstate state;
        if (rb.velocity.x > 0f)
        {
            state = Movementstate.running;
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            state = Movementstate.running;
            sr.flipX = true;
        }
        else
        {
            state = Movementstate.idle;
        }
        
        anim.SetInteger("state", (int)state);
    }
}
