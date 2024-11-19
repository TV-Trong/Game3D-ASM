using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour, ICharacter
{
    #region Base Stats Variable
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

    #region States Variable
    public EnemyStateMachine stateMachine;
    public EnemyIdleState idleState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public EnemySittingState sittingState;
    #endregion

    #region Animation Trigger
    private void AnimationTriggerEnvent(AnimationTriggerType triggerType)
    {
        stateMachine.currentState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        Taunt,
        Stagger,
        DetectPlayer,
        DropAggro,
        Attack
    }
    #endregion

    public enum EnemyClass
    {
        Warrior,
        Mage,
        Archer
    }
   
    private PlayerBehaviour player;
    private GameObject playerObject;
    private Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    public bool isPlayerInChaseRange {  get; set; }
    public bool isPlayerInAttackRange { get; set; }

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        healthSlider.gameObject.SetActive(false);
        maxHP = HP;
        maxPoise = poise;

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(playerObject, animator, this, stateMachine);
        chaseState = new EnemyChaseState(playerObject, animator, this, stateMachine);
        attackState = new EnemyAttackState(playerObject, animator, this, stateMachine);
        sittingState = new EnemySittingState(playerObject, animator, this, stateMachine);
    }

    private void Start()
    {
        stateMachine.Initialize(sittingState);
    }

    private void Update()
    {
        stateMachine.currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixUpdateState();
    }

    #region Methods
    public bool CheckCritChance(float critChance)
    {
        int random = Random.Range(0, 100);
        return (critChance > random);
    }

    public void DealDamage(GameObject target, float healthDamage, float poiseDamage, bool isCrit)
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

        if (HP <= 0) Die();
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

    public void Die()
    {
        Debug.Log(gameObject.name + " has Died!");
    }

    public void CheckPlayerInChaseRange(bool isTrue)
    {
        isPlayerInChaseRange = isTrue;
    }

    public void CheckPlayerInAttackRange(bool isTrue)
    {
        isPlayerInAttackRange = isTrue;
    }

    public void StartAttack()
    {
        //weapon.ReadyToDealDamage();
    }

    public void EndAttack()
    {
        //weapon.StopDealingDamage();
    }
    #endregion
}
