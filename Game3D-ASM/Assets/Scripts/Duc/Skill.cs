using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float manaUse;
    public float skillDamage;
    public float skillCritDamage;
    public float critRate;
    public SkillManager skillManager;
    public EnemyBehaviour enemy;
    public int coldDownTime = 0;
    public int s = 0;

    private bool isCrit;
    private float finalDamage;
    private float poiseDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(s == 1)
        {
            if (other.CompareTag("Enemy"))
            {
                isCrit = CheckCritChance(critRate);
                poiseDamage = skillDamage;
                finalDamage = skillDamage;
                if (isCrit) finalDamage = skillDamage * skillCritDamage;
                skillManager.DealDamage(other.gameObject, finalDamage, poiseDamage, isCrit);
            }
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (s == 2)
        {
            if (other.CompareTag("Enemy"))
            {
                isCrit = CheckCritChance(critRate);
                poiseDamage = skillDamage;
                finalDamage = skillDamage;
                if (isCrit) finalDamage = skillDamage * skillCritDamage;
                skillManager.DealDamage(other.gameObject, finalDamage, poiseDamage, isCrit);
            }
        }
    }

    public bool CheckCritChance(float critChance)
    {
        int random = Random.Range(0, 100);
        return (critChance > random);
    }
}
