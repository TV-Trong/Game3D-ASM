using UnityEngine;
using UnityEngine.AI;

namespace ASM19301
{
    public class EnemyAI : MonoBehaviour
    {
        public float patrolDuration = 8f;
        public float restDuration = 2f;
        public float patrolDistance = 10f;
        public float detectionRadius = 15f;
        public float attackRadius = 2f;
        public float attackCooldown = 1.5f;

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

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            patrolTimer = patrolDuration;
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
                agent.isStopped = true; // Dừng di chuyển khi tấn công
                isAttacking = true;
                attackTimer = attackCooldown; // Đặt lại thời gian chờ
            }

            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                if (detectedPlayer != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, detectedPlayer.transform.position);

                    if (distanceToPlayer > attackRadius && distanceToPlayer <= detectionRadius)
                    {
                        // Nếu Player ngoài tầm tấn công nhưng trong tầm truy đuổi
                        isAttacking = false; // Ngừng tấn công
                        SetAnimationState("isChasing"); // Chuyển sang trạng thái chasing
                        StartChasingPlayer(); // Quay lại trạng thái truy đuổi
                    }
                    else if (distanceToPlayer > detectionRadius)
                    {
                        // Nếu Player ngoài tầm truy đuổi
                        isAttacking = false; // Ngừng tấn công
                        ResetToPatrol(); // Quay lại tuần tra
                    }
                    else
                    {
                        // Nếu Player vẫn trong tầm tấn công, tiếp tục tấn công
                        attackTimer = attackCooldown;
                    }
                }
            }
        }

        private void ResetToPatrol()
        {
            if (isAttacking || isChasing)
            {
                isAttacking = false; // Ngừng tấn công
                isChasing = false;   // Ngừng truy đuổi
                detectedPlayer = null; // Xóa tham chiếu đến Player
                agent.isStopped = false; // Cho phép di chuyển tuần tra
                SetAnimationState("isPatrolling"); // Chạy animation tuần tra
                patrolTimer = patrolDuration;
                ChooseNewDirection();
            }
        }

        private void StartChasingPlayer()
        {
            if (!isChasing)
            {
                isChasing = true;
                isAttacking = false; // Ngừng tấn công khi bắt đầu truy đuổi
                SetAnimationState("isChasing");
                agent.isStopped = false; // Bắt đầu di chuyển
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

        private void SetAnimationState(string state)
        {
            // Reset tất cả các animation khác và chỉ bật animation đang hoạt động
            animator.SetBool("isPatrolling", state == "isPatrolling");
            animator.SetBool("isChasing", state == "isChasing");
            animator.SetBool("isAttacking", state == "isAttacking");
        }
    }
}
