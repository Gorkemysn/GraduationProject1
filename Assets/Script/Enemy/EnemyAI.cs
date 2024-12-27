using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    // FSM State'leri için enum
    private enum EnemyState { Patrol, Chase, Attack, Dodge }

    private EnemyState currentState; // Mevcut state

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Player Detection")]
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header("Awareness Bar")]
    public Slider awarenessSlider;
    public float awareness = 0f;
    public float maxAwareness = 100f;
    public float awarenessIncreaseRate = 10f;
    public float awarenessDecreaseRate = 5f;

    [Header("Dodge Settings")]
    public float dodgeDistance = 3f;
    public float dodgeCooldown = 2f;
    private bool canDodge = true;

    void Start()
    {
        // Baþlangýç state'i
        currentState = EnemyState.Patrol;
        animator.SetBool("isWalking", true);
        GoToNextPatrolPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolUpdate();
                break;

            case EnemyState.Chase:
                ChaseUpdate();
                break;

            case EnemyState.Attack:
                AttackUpdate();
                break;

            case EnemyState.Dodge:
                DodgeUpdate();
                break;
        }

        UpdateAwareness();
    }

    // Awareness Bar kontrolü
    private void UpdateAwareness()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            awareness += awarenessIncreaseRate * Time.deltaTime;
            awareness = Mathf.Clamp(awareness, 0, maxAwareness);

            if (awareness == maxAwareness && currentState != EnemyState.Chase)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                currentState = EnemyState.Chase;
            }
        }
        else
        {
            awareness -= awarenessDecreaseRate * Time.deltaTime;
            awareness = Mathf.Clamp(awareness, 0, maxAwareness);

            if (awareness == 0 && currentState == EnemyState.Chase)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                currentState = EnemyState.Patrol;
                GoToNextPatrolPoint();
            }
        }

        if (awarenessSlider != null)
        {
            awarenessSlider.value = awareness / maxAwareness;
        }
    }

    // Devriye State
    private void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.8f)
        {
            GoToNextPatrolPoint();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            currentState = EnemyState.Chase;
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    // Kovalama State
    private void ChaseUpdate()
    {
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("isRunning", false);
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer > detectionRange)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            currentState = EnemyState.Patrol;
            GoToNextPatrolPoint();
        }
    }

    // Saldýrma State
    private void AttackUpdate()
    {
        agent.ResetPath(); // Hareketi durdur
        transform.LookAt(player); // Oyuncuya doðru bak

        // Saldýrý animasyonu
        animator.SetTrigger("Attack");

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chase;
        }
        else if (canDodge && Random.value > 0.7f) // Rastgele bir ihtimalle dodge yap
        {
            currentState = EnemyState.Dodge;
        }
    }

    // Kaçýnma State
    private void DodgeUpdate()
    {
        if (!canDodge) return;

        canDodge = false;
        // Rastgele bir yöne doðru kaçýnma
        Vector3 dodgeDirection = (Random.insideUnitSphere * dodgeDistance).normalized;
        dodgeDirection.y = 0;

        Vector3 dodgePosition = transform.position + dodgeDirection;

        if (NavMesh.SamplePosition(dodgePosition, out NavMeshHit hit, dodgeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        // Kaçýnma animasyonu
        animator.SetTrigger("Dodge");

        // Kaçýnma iþlemi bittikten sonra saldýrýya geri dön
        Invoke(nameof(ResetDodge), dodgeCooldown);
        currentState = EnemyState.Attack;
    }

    private void ResetDodge()
    {
        canDodge = true;
    }
}
