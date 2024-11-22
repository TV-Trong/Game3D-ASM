using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySittingState_Animator : StateMachineBehaviour
{
    //private EnemyBehaviour enemyBehaviour;
    //private bool isAggroed;
    //private bool isIdle;
    //private float baseWeight = 1f;
    //private float currentWeight;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //enemyBehaviour = animator.GetComponent<EnemyBehaviour>();
        //isAggroed = animator.GetBool("IsAggroed");
        //isIdle = animator.GetBool("IsIdle");

        //if (enemyBehaviour.stateMachine.currentState == enemyBehaviour.idleState)
        //{
        //    currentWeight = baseWeight;
        //}
        //else
        //{
        //    currentWeight = 0f;
        //}
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (isAggroed && !isIdle)
        //{
        //    animator.SetLayerWeight(layerIndex, currentWeight);
        //    currentWeight -= Time.deltaTime;
        //    if (currentWeight <= 0f)
        //    {
        //        isIdle = true;
        //        animator.SetBool("IsIdle", true);
        //    }
        //}
        //else if (!isAggroed && !isIdle)
        //{
        //    animator.SetLayerWeight(layerIndex, currentWeight);
        //    currentWeight += Time.deltaTime;
        //    if (currentWeight > baseWeight)
        //    {
        //        isIdle = true;
        //        animator.SetBool("IsIdle", true);
        //    }
        //}
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
