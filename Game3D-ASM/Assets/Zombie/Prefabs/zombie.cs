using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ASM19301
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Enemy Stats")]
        [SerializeField] private float maxHP = 100f; // Máu tối đa
        [SerializeField] private float damage = 10f; // Sát thương cơ bản
        [SerializeField] private float attackCooldown = 1.5f; // Thời gian hồi chiêu tấn công
        [SerializeField] private float detectionRadius = 15f; // Bán kính phát hiện người chơi
        [SerializeField] private float attackRadius = 2f; // Bán kính tấn công

        [Header("AI & Movement")]
        public float patrolDuration = 8f;
        public float restDuration = 2f;
        public float patrolDistance = 10f;

        private NavMeshAgent agent;
        private Animator animator;
        private Vector3 patrolDirection;
        private float patrolTimer;
        private float restTimer;
        private float attackTimer;
        private bool isResting = false;
        private bool isChasing = false;
        private bool isAttacking = false;
        private GameObject detectedPlayer = null;

        private float currentHP;

        // Tham chiếu đến thanh HP UI
        [Header("UI Elements")]
        public Slider hpSlider;  // Thanh slider để hiển thị HP của quái
        private float damageDisplayTime = 2f;  // Thời gian hiển thị thanh HP sau khi nhận sát thương
        private float damageDisplayTimer = 0f; // Thời gian đếm ngược khi nhận sát thương

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            patrolTimer = patrolDuration;
            currentHP = maxHP; // Khởi tạo HP cho Enemy

            if (hpSlider != null)
            {
                hpSlider.maxValue = maxHP;  // Thiết lập giá trị tối đa cho thanh HP
                hpSlider.value = currentHP; // Thiết lập giá trị ban đầu cho thanh HP
                hpSlider.gameObject.SetActive(false); // Ẩn thanh HP lúc đầu
            }
            ChooseNewDirection();
            SetAnimationState("isPatrolling");
        }

        void Update()
        {
            DetectPlayer();

            if (detectedPlayer != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, detectedPlayer.transform.position);

                if (distanceToPlayer <= attackRadius)
                {
                    AttackPlayer();
                }
                else if (distanceToPlayer <= detectionRadius)
                {
                    StartChasingPlayer();
                    ChasePlayer();
                }
                else
                {
                    ResetToPatrol();
                }
            }
            else if (isResting)
            {
                Rest();
            }
            else
            {
                Patrol();
            }

            // Cập nhật thanh HP mỗi frame nếu cần thiết
            if (hpSlider != null)
            {
                if (damageDisplayTimer > 0)
                {
                    hpSlider.gameObject.SetActive(true); // Hiển thị thanh HP
                    damageDisplayTimer -= Time.deltaTime;
                }
                else
                {
                    hpSlider.gameObject.SetActive(false); // Ẩn thanh HP sau 2 giây
                }
                hpSlider.value = currentHP; // Cập nhật thanh HP
            }

            // Kiểm tra cheat khi nhấn phím "A"
            if (Input.GetKeyDown(KeyCode.A))
            {
                TakeDamage(50f); // Giảm 50 HP nếu nhấn phím A
            }
        }

        // Phương thức nhận sát thương
        public void TakeDamage(float damage)
        {
            currentHP -= damage;

            // Cập nhật thanh HP
            if (hpSlider != null)
            {
                hpSlider.value = currentHP; // Cập nhật thanh HP
            }

            // Hiển thị thanh HP khi nhận sát thương
            damageDisplayTimer = damageDisplayTime;

            // Kiểm tra khi máu về 0
            if (currentHP <= 0)
            {
                Die();
            }
        }

        // Phương thức chết
        private void Die()
        {
            SetAnimationState("isDead"); // Chạy animation chết
            Invoke(nameof(DestroyEnemy), 2f); // Xóa Enemy sau 2 giây
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject); // Xóa đối tượng Enemy
        }

        private void SetAnimationState(string state)
        {
            animator.SetBool("isPatrolling", state == "isPatrolling");
            animator.SetBool("isChasing", state == "isChasing");
            animator.SetBool("isAttacking", state == "isAttacking");
            animator.SetBool("isDead", state == "isDead");
        }

        private void Patrol()
        {
            if (!isChasing && !isAttacking)
            {
                SetAnimationState("isPatrolling");

                if (!agent.hasPath || agent.remainingDistance < 0.5f)
                {
                    Vector3 targetPosition = transform.position + patrolDirection * patrolDistance;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(targetPosition, out hit, patrolDistance, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                }

                patrolTimer -= Time.deltaTime;
                if (patrolTimer <= 0f)
                {
                    isResting = true;
                    restTimer = restDuration;
                    agent.ResetPath();
                }
            }
        }

        private void Rest()
        {
            SetAnimationState("isResting");
            restTimer -= Time.deltaTime;
            if (restTimer <= 0f)
            {
                isResting = false;
                patrolTimer = patrolDuration;
                ChooseNewDirection();
            }
        }

        private void ChasePlayer()
        {
            if (detectedPlayer != null)
            {
                agent.isStopped = false;
                agent.SetDestination(detectedPlayer.transform.position);
                SetAnimationState("isChasing");
            }
        }

        private void AttackPlayer()
        {
            if (!isAttacking && detectedPlayer != null)
            {
                SetAnimationState("isAttacking");
                agent.isStopped = true;
                isAttacking = true;
                attackTimer = attackCooldown;

                // Gây sát thương
                float healthDamage = Random.Range(10f, 20f); // Sát thương cơ bản
                float poiseDamage = 5f; // Sát thương poise
                bool isCrit = Random.Range(0f, 100f) < 20f; // 20% cơ hội chí mạng
                DealDamage(detectedPlayer, healthDamage, poiseDamage, isCrit);  // Gọi phương thức gây sát thương
            }

            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                if (detectedPlayer != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, detectedPlayer.transform.position);

                    if (distanceToPlayer > attackRadius && distanceToPlayer <= detectionRadius)
                    {
                        isAttacking = false;
                        StartChasingPlayer();
                    }
                    else if (distanceToPlayer > detectionRadius)
                    {
                        ResetToPatrol();
                    }
                    else
                    {
                        attackTimer = attackCooldown;
                    }
                }
            }
        }

        public void DealDamage(GameObject target, float healthDamage, float poiseDamage, bool isCrit)
        {
            PlayerBehaviour player = target.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.TakeDamage(healthDamage, poiseDamage, isCrit);  // Gọi phương thức của người chơi để nhận sát thương
            }
        }

        private void ResetToPatrol()
        {
            if (isAttacking || isChasing)
            {
                isAttacking = false;
                isChasing = false;
                detectedPlayer = null;
                agent.isStopped = false;
                SetAnimationState("isPatrolling");
                patrolTimer = patrolDuration;
                ChooseNewDirection();
            }
        }

        private void StartChasingPlayer()
        {
            if (!isChasing)
            {
                isChasing = true;
                isAttacking = false;
                SetAnimationState("isChasing");
                agent.isStopped = false;
            }
        }

        private void DetectPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    detectedPlayer = hit.gameObject;
                    return;
                }
            }
            detectedPlayer = null;
        }

        private void ChooseNewDirection()
        {
            float randomAngle = Random.Range(0f, 360f);
            patrolDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        }

        // Vẽ phạm vi detection và tấn công trên Editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
