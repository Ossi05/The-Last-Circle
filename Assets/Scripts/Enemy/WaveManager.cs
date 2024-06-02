using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static WaveManager Instance;

    public event EventHandler OnWaveCompleted;
    public event EventHandler OnAllWavesCompleted;

    // Spawner that spawns enemies outside of arena
    [SerializeField] Vector2 mapSize;
    [SerializeField] Wave[] waves;
    [SerializeField] Enemies[] enemies;
    [Space]
    [SerializeField] float spawnRate = 1f;
    [Header("Settings")]
    [SerializeField] float spawnDelay = 2f;

    float spawnOffset = 2;

    Wave currentWave;
    int currentWaveIndex = 0;

    float spawnTimer;
    float nextWaveTimer;
    bool allWavesCompleted;

    [System.Serializable]
    class Wave {
        public List<EnemySpawnData> enemySpawnData = new List<EnemySpawnData>();
    }

    [System.Serializable]
    public class EnemySpawnData {
        public EnemyType EnemyType;
        public int SpawnAmt;
        public int spawnAtOnce = 1;
    }

    [System.Serializable]
    public class Enemies {
        public EnemyType EnemyType;
        public GameObject[] enemyPrefab;
    }

    void Awake()
    {
        Instance = this;
        currentWave = waves[currentWaveIndex];
    }

    void Update()
    {   
        if (GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsCountDownToStart()) 
        {
            if (allWavesCompleted)
            {
                return;
            }

            if (nextWaveTimer <= 0)
            {
                // Spawn enemies outside mapSize every spawnRate seconds
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnRate)
                {
                    spawnTimer = 0;
                    // If no more wave is empty then spawn next wave
                    if (currentWave.enemySpawnData.Count == 0)
                    {   
                        if (AllEnemiesKilled())
                        {
                            WaveCompleted();
                        }
                        
                    }
                    else
                    {
                        SpawnEnemy();
                    }
                }
            }
            else
            {
                nextWaveTimer -= Time.deltaTime;
                if (nextWaveTimer <= 0)
                {
                    nextWaveTimer = 0;
                }
            }
        }
         
    }

    bool AllEnemiesKilled()
    {
        return BaseEnemy.EnemiesAlive.Count == 0;
    }

    void SpawnEnemy()
    {
        EnemySpawnData enemySpawnData = currentWave.enemySpawnData[UnityEngine.Random.Range(0, currentWave.enemySpawnData.Count)];
        
        if (enemySpawnData.SpawnAmt > 0)
        {   
            
            foreach (Enemies enemy in enemies)
            {
                if (enemy.EnemyType == enemySpawnData.EnemyType)
                {
                    GameObject enemyPrefab = enemy.enemyPrefab[UnityEngine.Random.Range(0, enemy.enemyPrefab.Length)].gameObject;         

                    Vector3 spawnPos = GetRandomSpawnPos();

                    for (int i = 0; i < enemySpawnData.spawnAtOnce; i++)
                    {
                        GameObject instantiatedEnemy = Instantiate(enemyPrefab, spawnPos + new Vector3(
                            UnityEngine.Random.Range(-spawnOffset, spawnOffset),
                            UnityEngine.Random.Range(-spawnOffset, spawnOffset), 0),
                            Quaternion.identity,
                            transform
                            );
                    }           
                    enemySpawnData.SpawnAmt--;
                }
            }
        }
        else
        {
            currentWave.enemySpawnData.Remove(enemySpawnData);
        }
        
    }

    Vector3 GetRandomSpawnPos()
    {
        int edge = UnityEngine.Random.Range(0, 4);
        float x = 0, y = 0;

        switch (edge)
        {
            case 0: // Spawn on the left edge
                x = -mapSize.x;
                y = UnityEngine.Random.Range(-mapSize.y, mapSize.y);
                break;
            case 1: // Spawn on the right edge
                x = mapSize.x;
                y = UnityEngine.Random.Range(-mapSize.y, mapSize.y);
                break;
            case 2: // Spawn on the bottom edge
                x = UnityEngine.Random.Range(-mapSize.x, mapSize.x);
                y = -mapSize.y;
                break;
            case 3: // Spawn on the top edge
                x = UnityEngine.Random.Range(-mapSize.x, mapSize.x);
                y = mapSize.y;
                break;
        }

        return new Vector3(x, y, 0);
    }

void WaveCompleted()
{   

    OnWaveCompleted?.Invoke(this, EventArgs.Empty);

    if (currentWaveIndex < waves.Length - 1)
    {   
        if (!AllEnemiesKilled()) { return; }
        Debug.Log("Wave Completed");
        currentWaveIndex++;
        currentWave = waves[currentWaveIndex];
        nextWaveTimer = spawnDelay;         
    }
    else
    {
        allWavesCompleted = true;
        OnAllWavesCompleted?.Invoke(this, EventArgs.Empty);
    }
}

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }

    public int GetTotalWaveCount()
    {
        return waves.Length;
    }

}
