using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float timeSinceLastAttack;
    private bool isRunning;
    private bool isWalking;
    public EnemyAttackState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.Attack)
        {
            if (enemyBehaviour.isPlayerInAttackRange)
            {
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0f;
            }
        }
    }

    public override void EnterState()
    {
        Debug.Log("You won't leave Skyrim alive!");
        animator.SetTrigger("Attack");
        timeSinceLastAttack = 0f;
        isWalking = true;
    }

    public override void ExitState()
    {
        Debug.Log("Must have been the wind");
        timeSinceLastAttack = 0f;
    }

    public override void FixUpdateState()
    {
        base.FixUpdateState();
    }

    public override void UpdateState()
    {
        if (playerObject != null && !animator.GetBool("LockMovement"))
        {
            enemyBehaviour.agent.SetDestination(playerObject.transform.position);
            if (isWalking) animator.SetFloat("InputMagnitude", 0.5f);
            else animator.SetFloat("InputMagnitude", 1f);
        }

        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > 15f)
        {
            enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.idleState);
        }
    }
}
