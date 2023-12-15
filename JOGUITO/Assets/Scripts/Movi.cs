using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movi : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isJumping;
    private bool isAttacking;
    public float speed;
    public float jumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        AttackPlayer();
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(movement * speed, rb.velocity.y);

        if (movement > 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if (movement < 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isAttacking)
        {
            anim.SetInteger("transition", 0);
        }

    }
    
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                anim.SetInteger("transition", 2);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
           
            }
        }
    }
    
    void AttackPlayer()
    {
        StartCoroutine("attack");
    }

    IEnumerator attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                anim.SetInteger("transition", 3);
                yield return new WaitForSeconds(0.5f);
                anim.SetInteger("transition", 0);
                isAttacking = false;
            }
        }
    }
}
