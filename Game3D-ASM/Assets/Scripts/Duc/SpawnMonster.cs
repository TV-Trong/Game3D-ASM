using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float spawnRadius = 5f;
    public int numberOfMonsters = 5;

    public GameObject player;
    public float interactDistance = 2f;
    public GameObject notificationUI;

    private GameObject notificationInstance;

    public int enemyCountBase = 0;
    public bool isActive = false;

    private void Start()
    {
        GameObject[] countEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCountBase = countEnemy.Length;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= interactDistance)
        {
            //notificationUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                SpawnMonsters();
                isActive = true;
            }
        }
        //else
        //{
        //    notificationUI.SetActive(false);
        //}

        Check();
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < numberOfMonsters; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y;

            GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Check()
    {
        if (isActive == true)
        {
            GameObject[] countEnemyCurrent = GameObject.FindGameObjectsWithTag("Enemy");
            if (countEnemyCurrent.Length <= enemyCountBase)
                Destroy(gameObject);
        }
    }
}
