using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private float timeSinceLastAttack;
    public EnemyChaseState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.Attack)
        {
            if (enemyBehaviour.isPlayerInAttackRange)
            {
                enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.attackState);
            }
        }
        if (triggerType == EnemyBehaviour.AnimationTriggerType.DropAggro)
        {
            if (!enemyBehaviour.isPlayerInChaseRange)
            {
                animator.SetBool("IsChasing", false);
                enemyBehaviour.agent.isStopped = true;
                enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.idleState);
            }
        }
    }

    public override void EnterState()
    {
        animator.SetBool("IsChasing", true);
        animator.SetBool("IsIdle", false);
        timeSinceLastAttack = 0f;
        enemyBehaviour.agent.isStopped = false;
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
        Vector3 targetPosition = new Vector3(playerObject.transform.position.x, enemyBehaviour.transform.position.y, playerObject.transform.position.z);

        enemyBehaviour.transform.LookAt(targetPosition);

        if (playerObject != null && !animator.GetBool("LockMovement"))
        {
            enemyBehaviour.agent.SetDestination(playerObject.transform.position);
        }

        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > 15f)
        {
            animator.SetBool("IsChasing", false);
            enemyBehaviour.agent.isStopped = true;
            enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.idleState);
        }
    }
}
