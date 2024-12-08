using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : EnemyBehaviour
{
    //EnemyClass myClass = EnemyClass.Warrior;
    public GameObject endPortal;
    private void OnDestroy()
    {
        if (endPortal != null) endPortal.SetActive(true);
    }
}
