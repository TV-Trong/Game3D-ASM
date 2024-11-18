using UnityEngine.UI;

public class EnemyState
{
    protected EnemyBehaviour enemyBehaviour;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState (EnemyBehaviour enemyBehaviour, EnemyStateMachine enemyStateMachine)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState () { }
    public virtual void FixUpdateState () { }
    public virtual void ExitState() { }
    public virtual void AnimationTriggerEvent(EnemyBehaviour.AnimationTriggerType triggerType) { }
}
