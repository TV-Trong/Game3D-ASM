using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryZone : MonoBehaviour
{
    [SerializeField] PlayerBehaviour playerBehaviour;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (playerBehaviour.isParrying)
            {
                playerBehaviour.isImmune = true;
            }
        }
    }
}
