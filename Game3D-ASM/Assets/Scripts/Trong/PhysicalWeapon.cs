using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public class PhysicalWeapon : MonoBehaviour
{
    [SerializeField] private float sharpness;
    [SerializeField] private PlayerBehaviour playerBehaviour;
    [SerializeField] private int piercing;
    [SerializeField] vThirdPersonController vTPC;
    private bool isAbleToDealDamage;
    private float playerDamage;
    private float playerCritChance;
    private float playerCritPower;
    private bool isCrit;
    private float finalDamage;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && isAbleToDealDamage)
        {
            SetUpPlayerStat();
            isCrit = playerBehaviour.CheckCritChance(playerCritChance);
            finalDamage = playerDamage * sharpness;
            if (isCrit) finalDamage *= playerCritPower;
            playerBehaviour.DealDamage(other.gameObject, finalDamage, isCrit);
        }
    }

    public void ReadyToDealDamage()
    {
        isAbleToDealDamage = true;
    }

    public void StopDealingDamage()
    {
        isAbleToDealDamage = false;
    }

    private void SetUpPlayerStat()
    {
        playerDamage = playerBehaviour.strength;
        playerCritChance = playerBehaviour.critChance;
        playerCritPower = playerBehaviour.critPower;
    }
    public void EnableMovement(bool isTrue)
    {
        vTPC.lockMovement = !isTrue;
        vTPC.lockRotation = !isTrue;
    }
}
