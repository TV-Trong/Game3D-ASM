using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private bool isRunning;
    private bool isWalking;
    public EnemyChaseState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        //if (triggerType == EnemyBehaviour.AnimationTriggerType.Attack)
        //{
        //    if (enemyBehaviour.isPlayerInAttackRange)
        //    {
        //        enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.attackState);
        //    }
        //}
    }

    public override void EnterState()
    {
        //Debug.Log("Never should have come here!");
        //if (enemyBehaviour.agent.speed <= 2)
        //{
        //    isWalking = true;
        //    isRunning = !isWalking;
        //}
        //animator.SetTrigger("DrawSword");
    }

    public override void ExitState()
    {
        //animator.SetFloat("InputMagnitude", 0f);
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();
    }

    public override void UpdateState()
    {
        //if (playerObject != null && !animator.GetBool("LockMovement"))
        //{
        //    enemyBehaviour.agent.SetDestination(playerObject.transform.position);
        //    if (isWalking) animator.SetFloat("InputMagnitude", 0.5f);
        //    else animator.SetFloat("InputMagnitude", 1f);
        //}
    }
}
