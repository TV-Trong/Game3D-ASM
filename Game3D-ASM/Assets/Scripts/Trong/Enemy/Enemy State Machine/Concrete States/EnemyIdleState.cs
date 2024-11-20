using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float timeUntilDropAggro = 3f;
    private float currentTimeUntilDropAggro;
    private float timeUntilChase = 3f;
    private float currentTimeUntilChase;
    private bool playerOutOfRange;
    public EnemyIdleState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.DropAggro)
        {
            if (!enemyBehaviour.isPlayerInChaseRange)
            {
                playerOutOfRange = true;
            }
        }
    }

    public override void EnterState()
    {
        animator.SetBool("IsAggroed", true);
        currentTimeUntilDropAggro = timeUntilDropAggro;
        currentTimeUntilChase = timeUntilChase;
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
        Debug.Log("Is in standing state");
        Vector3 targetPosition = new Vector3(playerObject.transform.position.x, enemyBehaviour.transform.position.y, playerObject.transform.position.z);

        enemyBehaviour.transform.LookAt(targetPosition);

        if (playerOutOfRange)
        {
            if (currentTimeUntilDropAggro > 0)
            {
                currentTimeUntilDropAggro -= Time.deltaTime;
            }
            else
            {
                playerOutOfRange = false;
                currentTimeUntilDropAggro = timeUntilDropAggro;
                enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.sittingState);
            }
        }
        else
        {
            currentTimeUntilChase -= Time.deltaTime;
            if (currentTimeUntilChase <= 0)
            {
                currentTimeUntilChase = timeUntilChase;
                enemyBehaviour.stateMachine.SwitchStage(enemyBehaviour.chaseState);
            }
        }
    }
}
