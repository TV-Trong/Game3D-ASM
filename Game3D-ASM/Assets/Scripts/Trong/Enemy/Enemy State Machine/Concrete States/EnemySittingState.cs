using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySittingState : EnemyState
{
    private Vector3 startingPosition;
    private float timeWaitBetweenMove = 10f;
    public EnemySittingState(GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine) : base(playerObject, animator, enemyBehaviour, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType)
    {
        if (triggerType == EnemyBehaviour.AnimationTriggerType.DetectPlayer)
        {
            if (enemyBehaviour.isPlayerInAggroRange)
            {
                if (enemyBehaviour.Action == EnemyBehaviour.EnemyType.Patrolling)
                {
                    enemyBehaviour.agent.SetDestination(enemyBehaviour.transform.position);
                }
                enemyStateMachine.SwitchStage(enemyBehaviour.idleState);
            }
        }
    }

    public override void EnterState()
    {
        animator.SetBool("IsAggroed", false);
        if (startingPosition == null) startingPosition = enemyBehaviour.transform.position;

        if (enemyBehaviour.Action == EnemyBehaviour.EnemyType.Patrolling)
        {
            Vector3 nextPoint = GetRandomPoint();
            enemyBehaviour.agent.SetDestination(nextPoint);
        }
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
        timeWaitBetweenMove -= Time.deltaTime;
        if (enemyBehaviour.Action == EnemyBehaviour.EnemyType.Patrolling && timeWaitBetweenMove <= 0)
        {
            Vector3 nextPoint = GetRandomPoint();
            enemyBehaviour.agent.SetDestination(nextPoint);
            timeWaitBetweenMove = 10f;
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomPoint = startingPosition + Random.insideUnitSphere * enemyBehaviour.patrolArea;
        NavMeshHit hit;
        // Ensure the point is on the NavMesh
        if (NavMesh.SamplePosition(randomPoint, out hit, enemyBehaviour.patrolArea, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return startingPosition; // Default to starting position if no valid point found
    }
}
