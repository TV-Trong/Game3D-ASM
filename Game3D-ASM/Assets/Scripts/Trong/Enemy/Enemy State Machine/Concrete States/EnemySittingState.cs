using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySittingState : EnemyState
{
    public EnemySittingState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.DetectPlayer)
        {
            if (enemyBehaviour.isPlayerInChaseRange)
            {
                enemyStateMachine.SwitchStage(enemyBehaviour.idleState);
            }
        }
    }

    public override void EnterState()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsAggroed", false);
        animator.SetTrigger("StandUp");
    }

    public override void ExitState()
    {
        
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
