using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private GameObject enemyPrefab;

    public Transform[] enemySpawnPoints;

    [SerializeField] private int enemyCount;

    private int initialEnemyCount;

    public float waitBeforeSpawnTime = 10f;

    // Used for initialization
    void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialEnemyCount = enemyCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        int index = 0;

        for (int i = 0; i < enemyCount; i++)
        {
            if (index >= enemySpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(enemyPrefab, enemySpawnPoints[index].position, Quaternion.identity);

            index++;
        }

        enemyCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawnTime);

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool enemy)
    {
        if (enemy)
        {
            enemyCount++;

            if (enemyCount > initialEnemyCount)
            {
                enemyCount = initialEnemyCount;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
