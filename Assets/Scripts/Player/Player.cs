using UnityEngine;
using System.Collections;

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

    private SpriteRenderer spriteRenderer;
    //[SerializeField]
    //private int extraJumps;
    //[SerializeField]
    //private int extraJumpsValue;
    
    private float attackTimer;

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

 
    public override void Start()
    {
        base.Start();
        //extraJumps = extraJumpsValue;
        myRigid = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        spriteRenderer = GetComponent<SpriteRenderer>();
=======
=======
>>>>>>> parent of bacbeb6... 맵이동
=======
>>>>>>> parent of bacbeb6... 맵이동
    }
    void Update()
    {
        if (transform.position.y <= -14f)
        {
            myRigid.velocity = Vector2.zero;
            transform.position = startPos;
        }
        HandleInput();
>>>>>>> parent of bacbeb6... 맵이동
    }
    
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();
            
            HandleMovement(horizontal);
        
            Flip(horizontal);
            
            HandleLayers();
        }
    }


    
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                myRigid.velocity = Vector2.zero;
                transform.position = startPos;
            }
        }
        HandleInput();
    }

    public void OnDead()
    {
        if (Dead != null)
        {
            Dead();
        }
    }
    private void HandleMovement(float horizontal)
    {
        if (myRigid.velocity.y < 0)
        {
            MyAnim.SetBool("land",true);
        }

        if (!Attack && (OnGround || airControl))
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
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
            return true;
        }
        else
            return false;
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
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 10;
            
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
                MyAnim.SetLayerWeight(1,0);
                MyAnim.SetTrigger("die");
            }
        }
        yield return null;
    }
}
