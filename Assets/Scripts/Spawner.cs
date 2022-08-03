using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemaingToSpawn;
    int enemiesRemaingAlive;
    float nextSpawnTime;

    private void Start()
    {
        NextWave();
    }
    private void Update()
    {
        if(enemiesRemaingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemaingToSpawn -= 1;
            nextSpawnTime = Time.deltaTime + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            // ???
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemaingAlive -= 1;

        if(enemiesRemaingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber += 1;
        if(currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemaingToSpawn = currentWave.enemyCount;
            enemiesRemaingAlive = enemiesRemaingToSpawn;
        }
        
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }

}
