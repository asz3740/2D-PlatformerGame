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

    // [SerializeField]
    // private Transform[] groundChecks;
    // [SerializeField] 
    // private float groundRadius;
    [SerializeField] 
    private Transform groundCheck;
    [SerializeField] 
    private Transform groundCheckL;
    [SerializeField] 
    private Transform groundCheckR;
    
    private bool isGrounded;
    // [SerializeField]
    // private LayerMask whatIsGround;
    private bool jump;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
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

        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        
        Flip(horizontal);
        
        HandleAttacks();
        
        HandleLayers();
        
        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
        if (myRigid.velocity.y < 0)
        {
            myAnim.SetBool("land",true);
        }
        
        if ( !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            myRigid.velocity = new Vector2(horizontal * movementSpeed, myRigid.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigid.AddForce(new Vector2(0, jumpForce));
            myAnim.SetTrigger("jump");
        }
        
        if (roll && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_Roll"))
        {
            myAnim.SetBool("roll",true);
        }
        else if(!this.myAnim.GetCurrentAnimatorStateInfo(0).IsName("Player_Roll"))
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
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
        jump = false;
    }

    private bool IsGrounded()
    {

        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            myAnim.ResetTrigger("jump");
            myAnim.SetBool("land", false);
            return true;
        }
        else
            return false;
    }





    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
        
    }
}
