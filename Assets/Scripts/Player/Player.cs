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
        get { return health <= 0; }
    }

 
    public override void Start()
    {
        base.Start();
        //extraJumps = extraJumpsValue;
        myRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.y <= -14f)
        {
            myRigid.velocity = Vector2.zero;
            transform.position = startPos;
        }
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TakingDamage)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();
            
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

    public override IEnumerator TakeDamage()
    {
        health -= 10;
        if (!IsDead)
        {
            MyAnim.SetTrigger("damage");
        }
        else
        {
            MyAnim.SetLayerWeight(1,0);
            MyAnim.SetTrigger("die");
        }
        yield return null;
    }
}
