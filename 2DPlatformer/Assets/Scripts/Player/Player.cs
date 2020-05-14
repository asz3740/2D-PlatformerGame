using UnityEngine;
using System.Collections;


public class Player : Character
{

    private static Player instance;
    
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    
    
    
    // [SerializeField]
    // private Transform[] groundChecks;
    // [SerializeField] 
    // private float groundRadius;
    //
    // [SerializeField]
    // private Transform[] groundPoints;
    [SerializeField] 
    private Transform groundCheck;
    [SerializeField] 
    private Transform groundCheckL;
    [SerializeField] 
    private Transform groundCheckR;
    
    // 점프
    //private bool isGrounded;
    // [SerializeField]
    // private LayerMask whatIsGround;
    //private bool jump;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int extraJumps;
    [SerializeField]
    private int extraJumpsValue;
    
    // 공격
    //private bool attack;
    [SerializeField]
    private int extraAttacks;
    private float attackTimer;
    [SerializeField] 
    private GameObject attackHitBox;
    


    
    public Rigidbody2D myRigid  { get; set; }
  
    public bool Roll { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }


 
    public override void Start()
    {
        attackHitBox.SetActive(false);
        base.Start();
        extraAttacks = 1;
        extraJumps = extraJumpsValue;
  
        myRigid = GetComponent<Rigidbody2D>();
    
    }

    void Update()
    {
        HandleInput();
        print(instance);
        print(Attack);
        print(myRigid.velocity.y);
        print("jump"+Jump);
        //Debug.Log(attackTimer);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        OnGround = IsGrounded();
        HandleMovement(horizontal);
        
        Flip(horizontal);
        
        //HandleAttacks(horizontal);
        
        HandleLayers();

    }

    private void HandleMovement(float horizontal)
    {
        if (myRigid.velocity.y < 0)
        {
            myAnim.SetBool("land",true);
        }

        if (!Attack)
        {
            myRigid.velocity = new Vector2(horizontal * movementSpeed, myRigid.velocity.y);
        }

        if (Jump && myRigid.velocity.y == 0)
        {
            myRigid.AddForce(new Vector2(0, jumpForce));
        }
        
        myAnim.SetFloat("speed", Mathf.Abs(horizontal));
        
    }

    IEnumerator HandleAttacks(float horizontal)
    {
        attackTimer += Time.deltaTime;
        if ((!this.myAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && attackTimer > 0.5) && extraAttacks == 1)
        {
            Debug.Log(extraAttacks);
            myAnim.SetTrigger("attack1");
            // StartCoroutine(DoAttack());
            myRigid.velocity = Vector2.zero;
            transform.Translate(new Vector3(0.1f * horizontal, myRigid.velocity.y));
            extraAttacks = 2;
            attackTimer = 0;
        }
        else if (extraAttacks > 0 && extraAttacks <= 4 && attackTimer <= 0.5)
        {
            Debug.Log(extraAttacks);
            myAnim.SetTrigger("attack" +extraAttacks);
            if (extraAttacks == 4)
            {
                transform.Translate(new Vector3(0.2f * horizontal, myRigid.velocity.y));
                extraAttacks = 1;
            }
            else
            {
                extraAttacks++;
                myRigid.velocity = Vector2.zero;
            }
            attackTimer = 0;
        }
        else
        {
            extraAttacks = 1;
        }
        
        yield return null;
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
            myAnim.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.J))
        {
            print("1."+Attack);
            myAnim.SetTrigger("attack1");
            print("2."+Attack);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myAnim.SetTrigger("roll");
        }
    }
    
    

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private bool IsGrounded()
    {
    
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
            (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            return true;
        }
        else
            return false;
    }





    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnim.SetLayerWeight(1, 1);
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
        
    }
}
