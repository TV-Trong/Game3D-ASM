using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySittingState : EnemyState
{
    public EnemySittingState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
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
