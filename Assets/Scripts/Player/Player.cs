using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public delegate void DeadEventHandler();

public class Player : Character
{
    private static Player instance;

    public event DeadEventHandler Dead;


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

    [SerializeField] 
    private Transform groundCheck;
    [SerializeField] 
    private Transform groundCheckL;
    [SerializeField] 
    private Transform groundCheckR;
    
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    
    private bool immortal = false;

    [SerializeField]
    private float immortalTime;

    public SpriteRenderer spriteRenderer;
    
    //[SerializeField]
    //private int extraJumps;
    //[SerializeField]
    //private int extraJumpsValue;

    private BoxCollider2D boxCollider;
    private Animator animator;

    public Rigidbody2D myRigid  { get; set; }
    public bool Roll { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }

    private Vector2 startPos;
    public override bool IsDead
    {
        get
        {
            if (health <= 0)
            {
                OnDead();
            }
            return health <= 0;
        }
    }

    public void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public override void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        base.Start();
        //extraJumps = extraJumpsValue;
        myRigid = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
        }
    }

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            HandleInput();

            OnGround = IsGrounded();
            print(OnGround);
            HandleMovement(horizontal);
        
            Flip(horizontal);
            
            HandleLayers();
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (myRigid.velocity.y < 0)
        {
            MyAnim.SetBool("land",true);
        }

        if (!Attack && !Roll)
        {
            myRigid.velocity = new Vector2(horizontal * movementSpeed, myRigid.velocity.y);
        }
       

        if (Jump && myRigid.velocity.y == 0)
        { 
            myRigid.AddForce(new Vector2(0, jumpForce));
        }
        
        MyAnim.SetFloat("speed", Mathf.Abs(horizontal));

        
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAnim.SetTrigger("jump");
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(0))
        {
            MyAnim.SetTrigger("attack");
        }

        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MyAnim.SetTrigger("roll");
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnim.SetTrigger("throw");
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
            print(myRigid.velocity.y);
            return true;
            
        }
        else
        {
            return false;
        }
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnim.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnim.SetLayerWeight(1, 0);
        }
    }
    
    public override void ThrowObject()
    { 
        base.ThrowObject();
    }
    
    private IEnumerator IndicateImmortal()
    {
        if (immortal)
        {
            for (int i = 0; i < 4; i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 10;
            print("player"+health);
            if (!IsDead)
            {
                MyAnim.SetTrigger("damage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }
            else
            {
                MyAnim.SetLayerWeight(1, 0);
                MyAnim.SetTrigger("die");
            }
            yield return null;
        }
    }

    public override void Death()
    {
        myRigid.velocity = Vector2.zero;
        MyAnim.SetTrigger("idle");
        health = 30;
        transform.position = startPos;
    }
}
