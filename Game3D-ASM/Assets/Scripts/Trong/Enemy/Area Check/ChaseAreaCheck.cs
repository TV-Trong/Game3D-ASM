using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAreaCheck : MonoBehaviour
{
    private GameObject player;
    private EnemyBehaviour enemyBehaviour;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) enemyBehaviour.CheckPlayerInChaseRange(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) enemyBehaviour.CheckPlayerInChaseRange(false);
    }
}
