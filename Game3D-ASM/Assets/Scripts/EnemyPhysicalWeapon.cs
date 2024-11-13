using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicalWeapon : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private float spinSpeed = 100f;
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private float sharpness;
    private float enemyDamage;
    private float enemyCritChance;
    private float enemyCritPower;
    private bool isCrit;
    private float finalDamage;

    private void Update()
    {
        transform.RotateAround(parentObject.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetUpEnemyStat();
            isCrit = (enemyBehaviour.CheckCritChance(enemyCritChance));
            finalDamage = enemyDamage * sharpness;
            if (isCrit) finalDamage *= enemyCritPower;
            enemyBehaviour.DealDamage(other.gameObject, finalDamage, isCrit);
        }
    }

    private void SetUpEnemyStat()
    {
        enemyDamage = enemyBehaviour.strength;
        enemyCritChance = enemyBehaviour.critChance;
        enemyCritPower = enemyBehaviour.critPower;
    }
}
