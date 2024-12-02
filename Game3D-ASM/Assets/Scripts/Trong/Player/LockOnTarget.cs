using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    public Transform player;  
    [HideInInspector] public Transform currentTarget;
    public float rotationSpeed = 5f;
    [HideInInspector] public bool lockOn;
    public LockEnemyZone lockEnemy;


    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lockOn = !lockOn;
            if (lockOn)
            {
                int random = Random.Range(0, lockEnemy.targets.Count);
                if (lockEnemy.targets.Count > 0)
                {
                    currentTarget = lockEnemy.targets[random];
                }
                else
                {
                    lockOn = !lockOn;
                }
            }
        }

        if (currentTarget != null && lockOn && lockEnemy.targets.Count > 0)
        {
            // Calculate the direction to the target
            Vector3 direction = (currentTarget.position - player.position).normalized;

            // Calculate the desired rotation
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Smoothly rotate the player
            player.rotation = Quaternion.Slerp(player.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
