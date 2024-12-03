using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SkillManager : MonoBehaviour
{
    public ParticleSystem skill1;
    public float distance = 0;
    public float searchRadius = 10f;
    private Animator anim;
    private EnemyBehaviour enemy;
    public bool isColliding = true;

    public int coldDownTime = 20;
    private float recoveryTime = 20f;
    private float elapsedTime = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= recoveryTime)
        {
            coldDownTime = 20;
            elapsedTime = 0f;
        }
        if (coldDownTime >= 20)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //isColliding = true;
                //var collisionModule = skill1.collision;
                //collisionModule.enabled = isColliding;
                CastSkill(skill1);
                anim.SetTrigger("Skill1");

                //collisionModule.enabled = isColliding;
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
        // Tìm các object có tag "Enemy" trong bán kính searchRadius
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

        GameObject nearestObject = null;
        float nearestDistance = Mathf.Infinity;

        // Tìm object gần nhất
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
        // Nếu tìm thấy object, sinh Particle System tại vị trí đó
        if (nearestObject != null)
        {
            skill.transform.position = nearestObject.transform.position;
            PlayPartical(skill);
            Quaternion rotation = Quaternion.LookRotation(skill.transform.position);
            //Instantiate(skill, nearestObject.transform.position, Quaternion.identity);
        }
        // Nếu không tìm thấy, sinh Particle System trước mặt player
        else
        {
            Vector3 spawnPosition = transform.position + transform.forward * distance;
            //Instantiate(skill, spawnPosition, Quaternion.identity);
            skill.transform.position = spawnPosition;
            PlayPartical(skill);
            Quaternion rotation = Quaternion.LookRotation(skill.transform.position);
        }
    }

    void PlayPartical(ParticleSystem skill)
    {
        //var collisionModule = skill.collision;
        //isColliding = true;
        //collisionModule.enabled = isColliding;
        skill.Play();
        coldDownTime = 0;
        //collisionModule.enabled = isColliding;
    }
}
