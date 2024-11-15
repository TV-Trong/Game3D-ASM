using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float allowBufferTime = 0.2f;
    [SerializeField] private float attackResetTime = 1.5f;
    private Animator animator;
    private bool isOnBufferTime;
    private int attackIndex;
    private float timeSinceLastAtk;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceLastAtk += Time.deltaTime;
        Debug.Log(timeSinceLastAtk);
    }

    private void OnFire()
    {
        if (timeSinceLastAtk > attackResetTime) attackIndex = 0;
        if (!isOnBufferTime)
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackPattern", attackIndex);

            isOnBufferTime = true;
            attackIndex++;

            Invoke("SetAttackable", allowBufferTime);
            timeSinceLastAtk = 0;
        }
        
        if (attackIndex > 2) attackIndex = 0;
    }
    private void SetAttackable()
    {
        isOnBufferTime = false;
    }
}
