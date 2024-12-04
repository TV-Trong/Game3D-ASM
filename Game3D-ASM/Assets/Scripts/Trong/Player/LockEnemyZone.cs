using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockEnemyZone : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    public LockOnTarget lockTarget;
    public LockOnCamera lockCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targets.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (lockTarget.currentTarget == other.transform)
            {
                lockTarget.lockOn = false;
                lockCamera.SetLockCamera(false);
            }
            targets.Remove(other.transform);
        }
    }
}
