using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float health = 100f;

    public bool isPlayer, isEnemy;

    private bool isDead;

    private EnemyAudio enemyAudio;

    void Awake()
    {
        if (isEnemy)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            // Get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if (isPlayer)
        {
            // Get player stats

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(float damage)
    {
        // If dead, don't execute anything below
        if (isDead)
        {
            return;
        }

        health -= damage;

        if (isPlayer)
        {
            // show the stats
            
        }

        if (isEnemy)
        {
            // If an enemy is in patrol
            if (enemyController.Enemy_State == EnemyState.PATROL)
            {
                // If -> The enemie's chase distance raises
                enemyController.chaseDistance = 50f;
            }
        }

        if (health <= 0f)
        {
            PlayerDied();

            isDead = true;
        }
    }

    void PlayerDied()
    {
        if (isEnemy)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 3f);

            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;

            StartCoroutine(DeadSound());
            /// EnemyManager
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.EnemyTag);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            /// Call enemy man
            
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if (tag == Tags.PlayerTag)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }
}
