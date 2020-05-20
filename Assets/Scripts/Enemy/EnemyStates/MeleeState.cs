using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy enemy;
    private float attackTimer;
    private float attackCoolDown = 3f;
    private bool canAttack = true;
    
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Execute()
    {
        Attack(); 
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
    
    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnim.SetTrigger("attack");
        }
    }
}
