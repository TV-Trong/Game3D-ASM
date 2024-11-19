using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackResetTime;
    public EnemyAttackState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        Debug.Log("You won't leave Skyrim alive!");
        animator.SetTrigger("Attack");
        attackResetTime = 4f;
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
        attackResetTime -= Time.deltaTime;
        if (attackResetTime <= 0) enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.chaseState); 
    }
}
