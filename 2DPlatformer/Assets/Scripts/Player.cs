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
    
    // 점프
    private bool isGrounded;
    // [SerializeField]
    // private LayerMask whatIsGround;
    private bool jump;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int extraJumps;
    [SerializeField]
    private int extraJumpsValue;
    
    // 공격
    private bool attack;
    [SerializeField]
    private int extraAttacks;
    private float attackTimer;
    [SerializeField] 
    private GameObject attackHitBox;
    
    void Start()
    {
        attackHitBox.SetActive(false);
        extraAttacks = 1;
        extraJumps = extraJumpsValue;
        facingRight = true;
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
     
        //Debug.Log(attackTimer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        
        Flip(horizontal);
        
        HandleAttacks(horizontal);
        
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


        if (jump && extraJumps > 0)
        {
            //isGrounded = false;
            extraJumps--;
            myRigid.AddForce(new Vector2(0, jumpForce));
            myAnim.SetTrigger("jump");
        }
        else if (isGrounded && jump && extraJumps == 0)
        {
            //isGrounded = false;
            myRigid.AddForce(new Vector2(0, jumpForce));
            myAnim.SetTrigger("jump");
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
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

    private int count1;
    private int count2;

    private void HandleAttacks(float horizontal)
    {
        attackTimer += Time.deltaTime;
        if ((attack && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && attackTimer > 0.5) || (attack && !this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && extraAttacks > 4))
        {
            count1++;
            Debug.Log("1-" +count1);
            myAnim.SetTrigger("attack1");
            // StartCoroutine(DoAttack());
            myRigid.velocity = Vector2.zero;
            transform.Translate(new Vector3(0.1f * horizontal, 0, 0));
            extraAttacks = 2;
            attackTimer = 0;
        }
        else if (attack && extraAttacks > 0 && extraAttacks <= 4 && attackTimer <= 0.5)
        {
            count2++;
            Debug.Log("2-" +count2);
            myAnim.SetTrigger("attack" +extraAttacks);
            // StartCoroutine(DoAttack());
            extraAttacks++;
            myRigid.velocity = Vector2.zero;
            transform.Translate(new Vector3(0.1f * horizontal, 0, 0));
            attackTimer = 0;
        }
    }
    // int count=0;
    // IEnumerator DoAttack()
    // {
    //     
    //     count++;
    //     Debug.Log(count);
    //     attackHitBox.SetActive(true);
    //     yield return new WaitForSeconds(0.1f);
    //     attackHitBox.SetActive(false);
    // }

    private void AttackHitBoxOn()
    {
        attackHitBox.SetActive(true);
    }
    
    private void AttackHitBoxOff()
    {
        attackHitBox.SetActive(false);
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
