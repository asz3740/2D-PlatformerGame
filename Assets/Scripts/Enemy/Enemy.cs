using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private float meleeRange;

    [SerializeField] 
    private float throwRange;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false;
        }
    }
    
    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }

            return false;
        }
    }


    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override void Start()
    {
        base.Start();
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
    }
    
    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
    }


    public override IEnumerator TakeDamage()
    {
        print("health"+health);
        health -= 10;
        if (!IsDead)
        {
            MyAnim.SetTrigger("damage");
        }
        else
        {
            MyAnim.SetTrigger(("die"));
            yield return null;
        }
        
    }
    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }


    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && !facingRight || xDir > 0 && facingRight)
            {
                print("타켓있음");
                ChangeDirection();
            }
        }
        
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = newState;
        
        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            MyAnim.SetFloat("speed",1);
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
         return !facingRight ? Vector2.right : Vector2.left ;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }
    
}
