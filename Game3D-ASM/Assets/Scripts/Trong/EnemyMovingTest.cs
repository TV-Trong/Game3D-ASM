using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingTest : MonoBehaviour
{
    public Transform target; // The target object to move towards
    public float speed = 5f; // The speed at which the object moves

    private Rigidbody rb;
    private Animator animator;
    private float idleTime;
    private float attackCD;
    private bool canAttack = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!animator.GetBool("Idle") && !animator.GetBool("IsAttacking"))
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Set the velocity in the direction of the target
            rb.velocity = direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)).normalized;
            transform.rotation = targetRotation;
        }
        else
        {
            idleTime += Time.deltaTime;
            animator.SetFloat("InputMagnitude", 0.5f - idleTime);
            if (idleTime > 2)
            {
                idleTime = 0;
                animator.SetBool("Idle", false);
            }
        }

        if (animator.GetBool("IsAttacking"))
        {
            idleTime = 0.5f;
        }
        if (!canAttack)
        {
            attackCD += Time.deltaTime;
            if (attackCD > 4f)
            {
                canAttack = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canAttack && !animator.GetBool("IsAttacking"))
        {
            animator.SetTrigger("Attack");
            canAttack = false;
            attackCD = 0;
        }
    }
}
