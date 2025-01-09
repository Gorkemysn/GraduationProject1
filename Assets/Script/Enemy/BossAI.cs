using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    private enum EnemyState { Patrol, Chase, Attack }

    private EnemyState currentState;

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

    void Start()
    {
        currentState = EnemyState.Patrol;
        SetAnimationState(true, false);
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
        }
    }

    private void SetAnimationState(bool isWalking, bool isRunning, bool isAttacking = false, bool isDodging = false)
    {
        animator.SetBool("isWalking", isWalking);

        if (isAttacking) animator.SetTrigger("Attack");
    }

    private void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.8f)
        {
            GoToNextPatrolPoint();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            SetAnimationState(false, true);
            currentState = EnemyState.Chase;
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void ChaseUpdate()
    {
        agent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            SetAnimationState(false, false);
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer > detectionRange)
        {
            SetAnimationState(true, false);
            currentState = EnemyState.Patrol;
            GoToNextPatrolPoint();
        }
    }

    private void AttackUpdate()
    {
        agent.ResetPath();
        transform.LookAt(player);

        SetAnimationState(false, false, true);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chase;
        }
    }
}
