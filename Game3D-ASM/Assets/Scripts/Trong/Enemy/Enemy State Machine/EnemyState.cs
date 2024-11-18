using UnityEngine;
using UnityEngine.UI;

public class EnemyState
{
    protected GameObject playerObject;
    protected Animator animator;
    protected EnemyBehaviour enemyBehaviour;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState (GameObject playerObject, Animator animator, EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine)
    {
        this.playerObject = playerObject;
        this.animator = animator;
        this.enemyBehaviour = enemyBehaviour;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState () { }
    public virtual void FixUpdateState () { }
    public virtual void ExitState() { }
    public virtual void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType) { }
}
