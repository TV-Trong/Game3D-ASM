using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(enemyBehaviour, enemyStateMachine)
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
