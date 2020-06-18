﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBehaviour : StateMachineBehaviour
{
    private Vector2 rollSize = new Vector2(3.7f,3f );
    private Vector2 rollOffset = new Vector2(-3f,-1.5f);

    private Vector2 size;
    private Vector2 offset;

    private BoxCollider2D boxCollider;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Roll = true;

        if (boxCollider == null)
        {
            boxCollider = Player.Instance.GetComponent<BoxCollider2D>();
            size = boxCollider.size;
            offset = boxCollider.offset;
        }

        boxCollider.size = rollSize;
        boxCollider.offset = rollOffset;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Roll = false;
        animator.ResetTrigger("roll");
        boxCollider.size = size;
        boxCollider.offset = offset;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
