using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour, ICharacter
{
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float MP { get; set; }
    [field: SerializeField] public float stamina { get; set; }
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float armor { get; set; }
    [field: SerializeField] public float critChance { get; set; }
    [field: SerializeField] public float critPower { get; set; }
    [field: SerializeField] public float iFrameTime { get; set; }
    [field: SerializeField] public bool isImmune { get; set; }
    [field: SerializeField] public Transform popupTextTransform { get; set; }
    [field: SerializeField] public Slider healthSlider { get; set; }
    public float maxHP { get; set; }

    [SerializeField] private PhysicalWeapon weapon;
    private EnemyBehaviour enemy;

    public bool CheckCritChance(float critChance)
    {
        int random = Random.Range(0, 100);
        return (critChance > random);
    }

    public void DealDamage(GameObject target, float power, bool isCrit)
    {
        enemy = target.GetComponent<EnemyBehaviour>();
        enemy.TakeDamage(power, isCrit);
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        if (!isImmune)
        {
            HP -= damage;
            DisplayDamageTaken(damage, isCrit);
            isImmune = true;
            Invoke("ResetIFrame", iFrameTime);
        }
    }

    public void StartAttack()
    {
        weapon.ReadyToDealDamage();
    }

    public void EndAttack()
    {
        weapon.StopDealingDamage();
    }

    public void ResetIFrame()
    {
        isImmune = false;
    }

    public void DisplayDamageTaken(float damage, bool isCrit)
    {
        GameObject popupTextObject = ObjectsPooler.instance.GetPooledObjects(0);
        popupTextObject.transform.position = popupTextTransform.position;
        PopupDamage popupDamage = popupTextObject.GetComponent<PopupDamage>();
        popupDamage.Setup(damage);
        popupDamage.SetDamageColor(Color.blue);
        if (isCrit) popupDamage.SetDamageColor(Color.cyan);
        popupTextObject.SetActive(true);
    }

    public void UpdateHealthbar()
    {
        throw new System.NotImplementedException();
    }
}
