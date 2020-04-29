using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    private Rigidbody2D rigid;

    private Vector3 movement;
    private bool isJumping = false;
    //private SpriteRenderer renderer;
    private Animator anim;
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        //renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
           anim.SetBool("isWalking",false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal")> 0 )
        {
            anim.SetBool("isWalking",true);
        }
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetTrigger("Jumping");
            anim.SetBool("isJumping",true);
        }
        
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        
        Debug.DrawRay(rigid.position, Vector3.down *2, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down * 2, 1,LayerMask.GetMask("GroundCheck"));

        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.name);
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(-1,1,1);
        }
        
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1,1,1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
        

           
    }

    void Jump()
    {
        if (!isJumping)
            return;
        rigid.velocity = Vector2.zero;
        
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 9 && rigid.velocity.y <0)
            anim.SetBool("isJumping",false);
    }
}
