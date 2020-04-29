using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigid;
    private Animator myAnim;
    [SerializeField] 
    private float movementSpeed;

    private bool attack;

    private bool roll;
    
    private bool facingRight;

    [SerializeField]
    private Transform[] groundChecks;
    [SerializeField] 
    private float groundRadius;

    private LayerMask whatIsGround;
    void Start()
    {
        facingRight = true;
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        HandleMovement(horizontal);
        
        Flip(horizontal);
        
        HandleAttacks();
        
        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (!myAnim.GetBool("roll") && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myRigid.velocity = new Vector2(horizontal * movementSpeed, myRigid.velocity.y);
        }
        if (roll && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
        {
            myAnim.SetBool("roll",true);
        }
        else if(!this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
        {
            myAnim.SetBool("roll",false);
        }
        
        myAnim.SetFloat("speed",Mathf.Abs(horizontal));
    }
    
    private void HandleAttacks()
    {
        if (attack && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnim.SetTrigger("attack");
            myRigid.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            roll = true;
        }
    }
    
    

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    private void ResetValues()
    {
        attack = false;
        roll = false;
    }

    private bool IsGrounded()
    {
        if (myRigid.velocity.y <= 0)
        {
            foreach (Transform check in groundChecks)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(check.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
