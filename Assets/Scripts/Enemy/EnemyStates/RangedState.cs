﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    private float throwTimer;
    private float throwCoolDown = 3f;
    private bool canThrow = true;
    
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowObject();
        
        if (enemy.Target != null)
        {
            Debug.Log("follow");
            enemy.Move();
        }
        // else
        // {
        //     enemy.ChangeState(new IdleState());
        // }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void ThrowObject()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnim.SetTrigger("throw");
        }
    }
}
