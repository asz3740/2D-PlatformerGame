﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration;


    public void Enter(Enemy enemy)
    {
        patrolDuration = Random.Range(1, 10);
        this.enemy = enemy;
    }
    
    public void Execute()
    {

        Patrol();
        Debug.Log("Patrol");
        enemy.Move();
        //
        if (enemy.Target != null && enemy.InThrowRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("ThrowObject"))
        {
            enemy.Target = Player.Instance.gameObject;
        }
    }
    
    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
