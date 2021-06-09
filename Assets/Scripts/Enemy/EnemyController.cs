using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;

    private EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 15f;
    private float currentChaseDistance;
    public float attackDistance = 2f;
    public float chaseAfterAttackDistance = 2f;

    public float patrolRadiusMinimum = 20f, patrolRadiusMaximum = 60f;
    public float patrolForThisTime = 15f;
    private float patrolTimer;

    public float waitBeforeAttack = 2f;
    private float attackTimer;

    private Transform target;

    public GameObject attackPoint;

    private EnemyAudio enemyAudio;

    void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PlayerTag).transform;

        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.PATROL;

        patrolTimer = patrolForThisTime;

        // When the enemy first gets to the player, attack right away
        attackTimer = waitBeforeAttack;

        // Memorize the value of chase distance so we can put it back
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        // Tell the map agent that an enemy can move
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;

        if (patrolTimer > patrolForThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }

        // Test the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);

            enemyState = EnemyState.CHASE;

            // Play spotted audio
            enemyAudio.PlayScreamSound();
        }
    }

    void Chase()
    {
        // Enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;

        // Set the player's position as the destination because we are chasing (running towards) the player
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }

        // If the distance between the enemy and the player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            // If -> Stop animations
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            // Else if -> Stop running
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;

            // Reset the patrol timer so that the function can calculate the new patrol destination right away
            patrolTimer = patrolForThisTime;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            enemyAnim.Attack();

            attackTimer = 0f;

            // Play attack sound
            enemyAudio.PlayAttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance)
        {
            enemyState = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        float randomRadius = Random.Range(patrolRadiusMinimum, patrolRadiusMaximum);

        Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
        randomDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, randomRadius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }
}
