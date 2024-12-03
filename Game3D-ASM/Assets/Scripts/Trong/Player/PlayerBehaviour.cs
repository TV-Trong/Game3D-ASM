using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour, ICharacter
{
    #region Base Stat Variables
    [field: SerializeField] public float HP { get; set; }
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
    #endregion


    public GameObject swordIdle;
    public GameObject swordOnCombat;
    [HideInInspector] public bool isOnCombatStage;
    [HideInInspector] public bool isParrying;
    [HideInInspector] public bool isParrySuccess;
    [HideInInspector] public float counterAttackTime = 1.5f;
    private PhysicalWeapon weapon;
    private EnemyBehaviour enemy;


    #region State Machine
    public PlayerStateMachine playerStateMachine;
    public PlayerIdleState idleState;
    public PlayerCombatState combatState;
    #endregion

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        playerStateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(playerStateMachine, this, gameObject, animator, swordIdle, swordOnCombat);
        combatState = new PlayerCombatState(playerStateMachine, this, gameObject, animator, swordIdle, swordOnCombat);
        weapon = swordOnCombat.GetComponent<PhysicalWeapon>();

        playerStateMachine.Initialize(idleState);
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
        if (!isImmune)
        {
            HP -= healthDamage;
            DisplayDamageTaken(healthDamage, isCrit);
            isImmune = true;
            Invoke("ResetIFrame", iFrameTime);

            poise -= poiseDamage;
            if (poise <= 0)
            {
                poise = maxPoise;
            }
        }

        if (HP <= 0) Die();
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
    public void Die()
    {
        Debug.Log(gameObject.name + " has Died!");
    }
    public void ParryEnemy(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.GetParried();
        isParrySuccess = true;
        SlashEffect slashEffect = GetComponent<SlashEffect>();
        slashEffect.PlayCounterEffect();
    }
    #endregion
}
