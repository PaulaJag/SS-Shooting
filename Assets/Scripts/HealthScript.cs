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

    public PlayerStats playerStats;

    public PlayerStats killStats;
    public int kills = 0;

    void Awake()
    {
        if (isEnemy)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            // Get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();
            killStats = GetComponent<PlayerStats>();
        }

        if (isPlayer)
        {
            // Get player stats
            playerStats = GetComponent<PlayerStats>();
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
            playerStats.DisplayHealthStats(health);
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
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 1f);

            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;

            StartCoroutine(DeadSound());

            // Enemy manager
            EnemyManager.instance.EnemyDied(true);

            // Increase the kills and display in player stats
            kills++;
            print("Kills: " + kills);
            // killStats.DisplayKills(kills);
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.EnemyTag);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // Call enemy manager
            EnemyManager.instance.StopSpawning();
            
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
