using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class AttackStateController : StateMachineBehaviour
{
    private PhysicalWeapon weapon;
    private Rigidbody _rigidbody;
    private vThirdPersonInput vTPInput;
    [SerializeField] private bool isMovementAllowed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidbody = animator.GetComponent<Rigidbody>();
        weapon = animator.GetComponentInChildren<PhysicalWeapon>();
        vTPInput = animator.GetComponent<vThirdPersonInput>();

        weapon.EnableMovement(isMovementAllowed);
        vTPInput.isAttacking = !isMovementAllowed;
        if (!isMovementAllowed) animator.SetBool("IsAttacking", true); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isMovementAllowed) if (_rigidbody != null) _rigidbody.drag += Time.deltaTime * 15;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isMovementAllowed) animator.SetBool("IsAttacking", false);
        _rigidbody.drag = 0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
