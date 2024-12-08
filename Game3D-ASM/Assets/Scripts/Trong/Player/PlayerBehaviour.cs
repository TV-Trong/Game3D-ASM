using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour, ICharacter
{
    #region Base Stat Variables
    [field: SerializeField] public float HP { get; set; }

    public float healthRegen = 5;
    [field: SerializeField] public float MP { get; set; }

    public float manaRegen = 5;
    [field: SerializeField] public float stamina { get; set; }
    public float staminaRegen = 5;
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
    public Slider manaSlider;
    public Slider staminaSlider;
    public float maxHP { get; set; }
    public float maxMP { get; set; }
    public float maxSP { get; set; }
    public float baseStrenght { get; set; }
    public float maxPoise { get; set; }
    public bool isDead;
    #endregion


    public GameObject swordIdle;
    public GameObject swordOnCombat;
    [HideInInspector] public bool isOnCombatStage;
    [HideInInspector] public bool isParrying;
    [HideInInspector] public bool isParrySuccess;
    [HideInInspector] public float counterAttackTime = 1.5f;
    private PhysicalWeapon weapon;
    private EnemyBehaviour enemy;
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI MP_textNumber;
    [SerializeField] private TextMeshProUGUI SP_textNumber;

    #region State Machine
    public PlayerStateMachine playerStateMachine;
    public PlayerIdleState idleState;
    public PlayerCombatState combatState;
    #endregion
    public AudioClip hit;
    public AudioClip deathSound;
    public AudioClip shealthSword;
    public AudioClip unshealth;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(playerStateMachine, this, gameObject, animator, swordIdle, swordOnCombat);
        combatState = new PlayerCombatState(playerStateMachine, this, gameObject, animator, swordIdle, swordOnCombat);
        weapon = swordOnCombat.GetComponent<PhysicalWeapon>();

        playerStateMachine.Initialize(idleState);
        counterAttackTime = 1.5f;
        maxHP = HP;
        maxMP = MP;
        maxSP = stamina;
        baseStrenght = strength;
        UpdateSlider();
        StartCoroutine(Regenerate());
    }

    private void Update()
    {
        playerStateMachine.currentState.Update();
        if (isParrySuccess)
        {
            counterAttackTime -= Time.deltaTime;
            if (counterAttackTime <= 0)
            {
                isParrySuccess = false;
                counterAttackTime = 1.5f;
            }
        }
    }

    #region Methods
    IEnumerator Regenerate()
    {
        while (!isDead)
        {
            if (MP < maxMP) MP += manaRegen;
            if (HP < maxHP) HP += healthRegen;
            if (stamina < maxSP) stamina += staminaRegen;
            UpdateSlider();
            yield return new WaitForSeconds(1);
        }
    }
    public void DrainStamina(float stamina)
    {
        this.stamina -= stamina;
        UpdateSlider();
    }
    public void GainStamina(float stamina)
    {
        this.stamina += stamina;
        UpdateSlider();
    }
    public bool CheckCritChance(float critChance)
    {
        int random = Random.Range(0, 100);
        return (critChance > random);
    }

    public void DealDamage(GameObject target, float healthDamage, float poiseDamage, bool isCrit)
    {
        enemy = target.GetComponent<EnemyBehaviour>();
        enemy.TakeDamage(healthDamage, poiseDamage, isCrit);
    }

    public void TakeDamage(float healthDamage, float poiseDamage, bool isCrit)
    {
        if (!isImmune && !isDead)
        {
            float finalDamage = healthDamage * (100 / (100 + armor));
            finalDamage = Mathf.Ceil(finalDamage);
            HP -= finalDamage;
            DisplayDamageTaken(finalDamage, isCrit);
            isImmune = true;
            Invoke("ResetIFrame", iFrameTime);
            SoundManager.instance.PlayClip(hit);
            poise -= poiseDamage;
            if (poise <= 0)
            {
                poise = maxPoise;
            }

            UpdateSlider();
        }

        if (HP <= 0 && !isDead)
        {
            Die();
            isDead = true;
            HP = 0;
            UpdateSlider();
        }
    }
    public void ConsumeMana(float mana)
    {
        MP -= mana;
        UpdateSlider();
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

    public void UpdateSlider()
    {
        MP = Mathf.Clamp(MP, 0, maxMP);
        HP = Mathf.Clamp(HP, 0, maxHP);
        stamina = Mathf.Clamp(stamina, 0, maxSP);
        healthSlider.value = HP / maxHP;
        textNumber.text = HP + "   /   " + maxHP;
        manaSlider.value = MP / maxMP;
        MP_textNumber.text = MP + "   /   " + maxMP;
        staminaSlider.value = stamina / maxSP;
        SP_textNumber.text = stamina + "   /   " + maxSP;
    }
    public void Die()
    {
        AnimationController anim = GetComponent<AnimationController>();
        HP = 0;
        UpdateSlider();
        anim.Die();
        SoundManager.instance.PlayClip(deathSound);
    }
    public void ParryEnemy(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.GetParried();
        isParrySuccess = true;
        SlashEffect slashEffect = GetComponent<SlashEffect>();
        slashEffect.PlayCounterEffect();
    }

    public void PlayShealthSound(bool isShealth)
    {
        if (isShealth)
        {
            SoundManager.instance.PlayClip(shealthSword);
        }
        else
        {
            SoundManager.instance.PlayClip(unshealth);
        }
    }
    public void StrengthUp(float multiplier)
    {
        strength *= multiplier;
    }
    public void ReturnToBaseStrength()
    {
        strength = baseStrenght;
    }
    #endregion
}
