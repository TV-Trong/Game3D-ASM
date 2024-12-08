using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SkillManager : MonoBehaviour
{
    public ParticleSystem skill1;
    public ParticleSystem skill2;
    public float distance = 0;
    public float searchRadius = 10f;
    private Animator anim;
    private EnemyBehaviour enemy;
    public PlayerBehaviour player;

    private int coldDownTime1 = 20;
    private int coldDownTime2 = 20;
    private float recoveryTime1 = 10f;
    private float recoveryTime2 = 20f;
    private float elapsedTime1 = 0f;
    private float elapsedTime2 = 0f;

    public AudioSource skillSound1;
    public AudioSource skillSound2;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        elapsedTime1 += Time.deltaTime;
        if (elapsedTime1 >= recoveryTime1)
        {
            coldDownTime1 = 20;
            elapsedTime1 = 0f;
        }
        if (coldDownTime1 >= 20)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Skill skill = skill1.GetComponent<Skill>();
                if (player.MP >= skill.manaUse)
                {
                    player.ConsumeMana(skill.manaUse);
                    CastSkill(skill1);
                    coldDownTime1 = 0;
                    skillSound1.Play();
                }
                CastSkill(skill1);
                coldDownTime1 = 0;
                skillSound1.Play();
            }
        }

        elapsedTime2 += Time.deltaTime;
        if (elapsedTime2 >= recoveryTime2)
        {
            coldDownTime2 = 20;
            elapsedTime2 = 0f;
        }
        if (coldDownTime2 >= 20)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Skill skill = skill2.GetComponent<Skill>();
                if (player.MP >= skill.manaUse)
                {
                    player.ConsumeMana(skill.manaUse);
                    CastSkill(skill2);
                    coldDownTime2 = 0;
                    skillSound2.Play();
                }
            }
        }
    }

    public void DealDamage(GameObject target, float healthDamage, float poiseDamage, bool isCrit)
    {
        enemy = target.GetComponent<EnemyBehaviour>();
        enemy.TakeDamage(healthDamage, poiseDamage, isCrit);
    }

    void CastSkill(ParticleSystem skill)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

        GameObject nearestObject = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = collider.gameObject;
                }
            }
        }
        if (nearestObject != null)
        {
            skill.transform.position = nearestObject.transform.position;
            PlayPartical(skill);
            Quaternion rotation = Quaternion.LookRotation(skill.transform.position);
        }
        else
        {
            Vector3 spawnPosition = transform.position + transform.forward * distance;
            skill.transform.position = spawnPosition;
            PlayPartical(skill);
            Quaternion rotation = Quaternion.LookRotation(skill.transform.position);
        }
    }

    void PlayPartical(ParticleSystem skill)
    {
        skill.Play();
    }
}
