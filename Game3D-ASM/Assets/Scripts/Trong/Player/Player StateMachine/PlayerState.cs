
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected PlayerBehaviour playerBehaviour;
    protected GameObject playerGameObject;
    protected Animator animator;
    protected GameObject swordIdle;
    protected GameObject swordCombat;

    public PlayerState(PlayerStateMachine playerStateMachine, PlayerBehaviour playerBehaviour, GameObject playerGameObject, Animator animator, GameObject swordIdle, GameObject swordCombat)
    {
        this.playerStateMachine = playerStateMachine;
        this.playerBehaviour = playerBehaviour;
        this.playerGameObject = playerGameObject;
        this.animator = animator;
        this.swordIdle = swordIdle;
        this.swordCombat = swordCombat;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
