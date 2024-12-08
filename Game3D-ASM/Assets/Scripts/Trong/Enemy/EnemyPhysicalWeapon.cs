using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class EnemyPhysicalWeapon : MonoBehaviour
{
    [SerializeField] private float sharpness;
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private int piercing;
    private bool isAbleToDealDamage;
    private float enemyDamage;
    private float enemyCritChance;
    private float enemyCritPower;
    private bool isCrit;
    private float finalDamage;
    private float poiseDamage;
    private PlayerBehaviour playerBehaviour;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isAbleToDealDamage)
        {
            playerBehaviour = other.GetComponent<PlayerBehaviour>();
            if (playerBehaviour.isImmune && playerBehaviour.isParrying)
            {
                playerBehaviour.ParryEnemy(enemyBehaviour);
            }
            SetUpEnemyStat();

            finalDamage = enemyDamage * sharpness;

            isCrit = (enemyBehaviour.CheckCritChance(enemyCritChance));
            if (isCrit) finalDamage *= enemyCritPower;

            enemyBehaviour.DealDamage(other.gameObject, finalDamage, 0, isCrit);
        }
    }

    private void SetUpEnemyStat()
    {
        enemyDamage = enemyBehaviour.strength;
        enemyCritChance = enemyBehaviour.critChance;
        enemyCritPower = enemyBehaviour.critPower;
    }
    public void ReadyToDealDamage()
    {
        isAbleToDealDamage = true;
    }

    public void StopDealingDamage()
    {
        isAbleToDealDamage = false;
    }
}
