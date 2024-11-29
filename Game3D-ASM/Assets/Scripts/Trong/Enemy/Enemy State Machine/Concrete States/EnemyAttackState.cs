using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float timeSinceLastAttack;
    public EnemyAttackState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.Attack)
        {
            if (enemyBehaviour.isPlayerInAttackRange)
            {
                if (timeSinceLastAttack > 7)
                {
                    animator.SetTrigger("Attack");
                    timeSinceLastAttack = 0f;
                }
            }
        }
    }

    public override void EnterState()
    {
        Debug.Log("You won't leave Skyrim alive!");
        animator.SetTrigger("Attack");
        timeSinceLastAttack = 0f;
        enemyBehaviour.agent.isStopped = false;
    }

    public override void ExitState()
    {
        timeSinceLastAttack = 0f;
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();
    }

    public override void UpdateState()
    {
        Debug.Log("Is in attack state");
        Vector3 targetPosition = new Vector3(playerObject.transform.position.x, enemyBehaviour.transform.position.y, playerObject.transform.position.z);

        enemyBehaviour.transform.LookAt(targetPosition);

        if (playerObject != null && !animator.GetBool("LockMovement"))
        {
            enemyBehaviour.agent.SetDestination(playerObject.transform.position);
            animator.SetBool("IsChasing", true);
        }

        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > 15f)
        {
            Debug.Log("Come back here scum!");
            animator.SetBool("IsChasing", false);
            enemyBehaviour.agent.isStopped = true;
            enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.idleState);
        }
    }
}
