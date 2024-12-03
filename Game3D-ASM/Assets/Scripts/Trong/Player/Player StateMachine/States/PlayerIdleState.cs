using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, PlayerBehaviour playerBehaviour, GameObject playerGameObject, Animator animator, GameObject swordIdle, GameObject swordCombat) : base(playerStateMachine, playerBehaviour, playerGameObject, animator, swordIdle, swordCombat)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        if (!animator.GetBool("IsDead"))
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                playerBehaviour.isOnCombatStage = true;
                playerBehaviour.swordIdle.SetActive(false);
                playerBehaviour.swordOnCombat.SetActive(true);
                animator.SetTrigger("DrawSword");
                playerStateMachine.SwitchState(playerBehaviour.combatState);
            }
        }
    }
}
