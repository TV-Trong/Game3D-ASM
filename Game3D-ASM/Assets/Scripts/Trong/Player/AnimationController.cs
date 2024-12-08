using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float allowBufferTime = 0.2f;
    [SerializeField] private float attackResetTime = 1.5f;
    public GameObject deathCam;
    private PlayerBehaviour playerBehaviour;
    private Animator animator;
    private bool isOnBufferTime;
    private int attackIndex;
    private float timeSinceLastAtk;
    public GameObject deathCanvas;

    LockOnTarget lockOnTarget;
    ActionController controller;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerBehaviour = GetComponent<PlayerBehaviour>();
        lockOnTarget = FindObjectOfType<LockOnTarget>();
        controller = FindObjectOfType<ActionController>();
    }

    private void Update()
    {
        timeSinceLastAtk += Time.deltaTime;
        Parry();
    }

    private void OnFire()
    {
        if (!animator.GetBool("IsDead"))
        {
            if (playerBehaviour.isParrySuccess)
            {
                playerBehaviour.GainStamina(30); 
                animator.SetTrigger("CounterSlash");
                playerBehaviour.isParrySuccess = false;
                playerBehaviour.counterAttackTime = 1f;
                return;
            }
            if (playerBehaviour.isOnCombatStage && !ActionController.isGameStop)
            {
                if (timeSinceLastAtk > attackResetTime) attackIndex = 0;
                if (!isOnBufferTime)
                {
                    switch (attackIndex)
                    {
                        case 0: if (playerBehaviour.stamina >= 15) playerBehaviour.DrainStamina(15); else return; break;
                        case 1: if (playerBehaviour.stamina >= 15) playerBehaviour.DrainStamina(20); else return; break;
                        case 2: if (playerBehaviour.stamina >= 20) playerBehaviour.DrainStamina(20); else return; break;
                        default: break;
                    }

                    animator.SetTrigger("Attack");
                    animator.SetInteger("AttackPattern", attackIndex);

                    isOnBufferTime = true;
                    attackIndex++;

                    Invoke("SetAttackable", allowBufferTime);
                    timeSinceLastAtk = 0;
                }

                if (attackIndex > 2) attackIndex = 0;
            }
        }
        

    }

    private void Parry()
    {
        if (Input.GetMouseButtonDown(1) && playerBehaviour.isOnCombatStage)
        {
            animator.SetBool("Parry", true);
            playerBehaviour.isParrying = true;
        }
        if (Input.GetMouseButtonUp(1) && playerBehaviour.isOnCombatStage)
        {
            animator.SetBool("Parry", false);
            playerBehaviour.isParrying = false;
            playerBehaviour.isImmune = false;
        }
    }
    public void SetParry()
    {
        playerBehaviour.isParrying = true;
    }

    private void SetAttackable()
    {
        isOnBufferTime = false;
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        animator.SetBool("IsDead", true);
        lockOnTarget.lockOn = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        controller.RemoveAllBinding();
        vThirdPersonInput input = GetComponent<vThirdPersonInput>();
        input.enabled = false;
        deathCam.SetActive(true);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        deathCanvas.SetActive(true);
    }
}
