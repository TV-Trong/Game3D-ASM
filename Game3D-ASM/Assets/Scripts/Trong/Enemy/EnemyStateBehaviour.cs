using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private bool isAllowedMoving;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isStagger;
    private float walkTime;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isAllowedMoving) animator.SetBool("Idle", false);
        if (isAttack) animator.SetBool("IsAttacking", true);
        if (isStagger) 
        { 
            animator.SetBool("Idle", true);
            animator.SetBool("IsStagger", true);
        }

    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isAllowedMoving)
        {
            float mag = Mathf.Clamp(walkTime, 0f, 0.5f);
            if (!animator.GetBool("Idle")) 
            {   
                walkTime += Time.deltaTime;
                animator.SetFloat("InputMagnitude", mag);
            }

            if (walkTime > 5)
            {
                animator.SetBool("Idle", true);
                walkTime = 0;
            }
        }
        if (animator.GetBool("IsAttacking"))
        {
            walkTime = 0;
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("IsAttacking") && !isAllowedMoving)
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
