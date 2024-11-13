using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float allowBufferTime = 0.2f;
    [SerializeField] private string part1Name;
    [SerializeField] private string part2Name;
    private Animator animator;
    private bool isAttackable = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnFire()
    {
        if (isAttackable)
        {
            animator.SetTrigger("Attack");
            isAttackable = false;
            Invoke("SetAttackable", allowBufferTime);
        }
    }
    private void SetAttackable()
    {
        isAttackable = true;
    }
}
