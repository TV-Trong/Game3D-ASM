using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerState
{
    public PlayerCombatState(PlayerStateMachine playerStateMachine, PlayerBehaviour playerBehaviour, GameObject playerGameObject, Animator animator, GameObject swordIdle, GameObject swordCombat) : base(playerStateMachine, playerBehaviour, playerGameObject, animator, swordIdle, swordCombat)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerBehaviour.isOnCombatStage = false;
            playerBehaviour.swordIdle.SetActive(true);
            playerBehaviour.swordOnCombat.SetActive(false);
            animator.SetTrigger("ShealthSword");
            playerStateMachine.SwitchState(playerBehaviour.idleState);
        }
    }
}
