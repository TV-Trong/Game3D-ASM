using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        Debug.Log("Never should have come here!");
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
