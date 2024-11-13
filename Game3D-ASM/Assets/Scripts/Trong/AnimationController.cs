using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private bool isMoving;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnFire()
    {
        Vector3 x = new Vector3(0.1f, .1f, .1f);
        isMoving = (Mathf.Abs(rb.velocity.x) > x.x || Mathf.Abs(rb.velocity.z) > x.z);
        animator.SetBool("IsMoving", isMoving);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger("SpinAttack");
            return;
        }
        animator.SetTrigger("Attack");
    }
}
