using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController2D : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;

    private bool isGrounded = true;
    [SerializeField] 
    private Transform groundCheck;
    [SerializeField] 
    private Transform groundCheckL;
    [SerializeField] 
    private Transform groundCheckR;

    [SerializeField] 
    private GameObject attackHitBox;
    private bool isAttack = false;

    
    

    private void Awake()
    {
        //if( comboParams == null || (comboParams != null && comboParams.Length == 0))
             //comboParams = new string[]{ "Attack1", "Attack2", "Attack3", "Attack4" };
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        attackHitBox.SetActive(false);
    }

 

        private float attackTime = 0f;
        private float attackcool = 0.4f;
        int attackcount = 1;

        void Update()
        {
            if (attackTime <= 0 && !isAttack)
            {
                if (Input.GetKey(KeyCode.Z) && attackcount == 1)
                {
                    isAttack = true;
                    anim.Play("Player_Attack1");
                    attackTime = attackcool;
                    attackcount++;
                    Debug.Log("1번공격");
                    StartCoroutine(DoAttack());
                }

                else if (Input.GetKey(KeyCode.Z) && -0.7 <= attackTime && attackcount > 1)
                {
                    isAttack = true;
                    Debug.Log("2번공격");
                    anim.Play("Player_Attack" + attackcount);
                    attackTime = attackcool;
                    attackcount++;
                    Debug.Log(attackcount);
                    StartCoroutine(DoAttack());
                    if (attackcount == 5)
                    {
                        attackcount = 1;
                    }
                }
                else
                {
                    attackcount = 1;
                }
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }

        IEnumerator DoAttack()
        {
            attackHitBox.SetActive(true);
            yield return new WaitForSeconds(.2f);
            attackHitBox.SetActive(false);
            isAttack = false;
        }



        // if (Input.GetKey(KeyCode.A) && !isAttack && attackTime >= attackcool)
            // {
            //     attackTime += Time.deltaTime;
            //     isAttack = true;
            //     anim.Play("Player_Attack1");
            //     if (Input.GetKey(KeyCode.A) && attackTime >= attackcool && attackTime <= 0.8f)
            //     {
            //         anim.Play("Player_Attack2");
            //     }
            //     else
            //     {
            //         isAttack = false;
            //         attackTime = 0f;
            //         
            //     }
            // }
        


        void FixedUpdate()
        {
            if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            if (Input.GetKey("right"))
            {
                rigid.velocity = new Vector2(2, rigid.velocity.y);
            
                if(isGrounded && !isAttack)
                    anim.Play("Player_Run");
          
                transform.localScale = new Vector3(1, 1,1);
            }
        
            else if (Input.GetKey("left"))
            {
                rigid.velocity = new Vector2(-2, rigid.velocity.y);
            
                if(isGrounded && !isAttack)
                    anim.Play("Player_Run");
            
                transform.localScale = new Vector3(-1, 1,1);
            }

            else
            {
                if(isGrounded && !isAttack)
                    anim.Play("Player_Idle");
            
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }

            if (Input.GetKey("space") && isGrounded)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 7);
                anim.Play("Player_Jump");
            }
        }
    
}
