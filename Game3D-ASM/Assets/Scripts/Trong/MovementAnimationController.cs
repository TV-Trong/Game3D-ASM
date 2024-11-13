using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class MovementAnimationController : StateMachineBehaviour
{
    private vThirdPersonController vTPController;
    private PhysicalWeapon weapon;
    private Rigidbody _rigidbody;
    private vThirdPersonInput vTPInput;
    private float inputMagnitude = 0.5f;
    [SerializeField] private bool isMovementAllowed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vTPController = animator.GetComponent<vThirdPersonController>();
        if (!isMovementAllowed)
        {
            inputMagnitude = 0.5f;
            vTPController.SetMagnitude(inputMagnitude);
        }
        _rigidbody = animator.GetComponent<Rigidbody>();
        weapon = animator.GetComponentInChildren<PhysicalWeapon>();
        weapon.EnableMovement(isMovementAllowed);
        vTPInput = animator.GetComponent<vThirdPersonInput>();
        vTPInput.isAttacking = !isMovementAllowed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isMovementAllowed) 
        { 
            if (_rigidbody != null) _rigidbody.drag += Time.deltaTime * 5;
            inputMagnitude -= Time.deltaTime / 5;
            inputMagnitude = Mathf.Clamp(inputMagnitude, 0, inputMagnitude);
            Debug.Log(inputMagnitude);
            vTPController.SetMagnitude(inputMagnitude);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
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
