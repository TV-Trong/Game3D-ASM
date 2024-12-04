using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLifeTime : MonoBehaviour
{
    public float lifetime = 20f;
    public float respawnTime = 2f;
    public ParticleSystem skill;

    void Start()
    {
        //StartCoroutine(DisableAfterTime());
    }

    private void Update()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        StopParticleSystem();
        yield return new WaitForSeconds(respawnTime);
        StopParticleSystem();
    }
    IEnumerator RespawnCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => !skill.isPlaying); // Chờ đến khi Particle System dừng
            yield return new WaitForSeconds(respawnTime); // Chờ respawnTime giây
            StartParticleSystem();
        }
        while (false)
        {
            yield return new WaitUntil(() => !skill.isPlaying);
            yield return new WaitForSeconds(lifetime);
            StopParticleSystem();
        }    
    }

    public void StartParticleSystem()
    {
        skill.gameObject.SetActive(true);
    }

    public void StopParticleSystem()
    {
        skill.gameObject.SetActive(false);
    }
}
