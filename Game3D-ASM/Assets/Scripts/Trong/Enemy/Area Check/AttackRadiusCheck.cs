using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusCheck : MonoBehaviour
{
    private EnemyBehaviour enemyBehaviour;

    private void Awake()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) enemyBehaviour.SetPlayerInAttackRange(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) enemyBehaviour.SetPlayerInAttackRange(false);
    }
}
