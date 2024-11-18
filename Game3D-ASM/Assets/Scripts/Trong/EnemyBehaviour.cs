using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour, ICharacter
{
    [field:SerializeField] public float HP { get; set; }
    [field: SerializeField] public float MP { get; set; }
    [field: SerializeField] public float stamina { get; set; }
    [field: SerializeField] public float moveSpeed { get; set; }
    [field: SerializeField] public float strength { get; set; }
    [field: SerializeField] public float armor { get; set; }
    [field: SerializeField] public float poise { get; set; }
    [field: SerializeField] public float critChance { get; set; }
    [field: SerializeField] public float critPower { get; set; }
    [field: SerializeField] public float iFrameTime { get; set; }
    [field: SerializeField] public bool isImmune { get; set; }
    [field: SerializeField] public Transform popupTextTransform { get; set; }
    [field: SerializeField] public Slider healthSlider { get; set; }
    public float maxHP { get; set; }
    public float maxPoise { get; set; }

    [SerializeField] EnemyPhysicalWeapon weapon;
    private PlayerBehaviour player;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        healthSlider.gameObject.SetActive(false);
        maxHP = HP;
        maxPoise = poise;
    }

    public bool CheckCritChance(float critChance)
    {
        int random = Random.Range(0, 100);
        return (critChance > random);
    }

    public void DealDamage(GameObject target, float healthDamage, float poiseDamage,bool isCrit)
    {
        player = target.GetComponent<PlayerBehaviour>();
        player.TakeDamage(healthDamage, poiseDamage, isCrit);
    }

    public void TakeDamage(float healthDamage, float poiseDamage, bool isCrit)
    {
        if (!isImmune)
        {
            isImmune = true;
            HP -= healthDamage;
            DisplayDamageTaken(healthDamage, isCrit);
            UpdateHealthbar();
            Invoke("ResetIFrame", iFrameTime);

            poise -= poiseDamage;
            Debug.Log(gameObject.name + " poise: " + poise);
            if (poise <= 0)
            {
                poise = maxPoise;
                animator.SetTrigger("Stagger");
                Debug.Log("Stagger");
            }
        }
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
        popupDamage.SetDamageColor(Color.red);
        if (isCrit) popupDamage.SetDamageColor(Color.yellow);
        popupTextObject.SetActive(true);
    }

    public void UpdateHealthbar()
    {
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = HP / maxHP;
    }
    public void StartAttack()
    {
        weapon.ReadyToDealDamage();
    }

    public void EndAttack()
    {
        weapon.StopDealingDamage();
    }
}
