using System.Collections.Generic;
using SimpleSurvivors.Extensions;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Timer timer;
        [SerializeField] private Transform lootParent;
        [SerializeField] private WaveSO[] waves;

        private readonly List<EnemyMovement> _pausedMovements = new();
        private bool _isEnabled = false;
        private int _waveIndex = 0;

        private void Start()
        {
            ResetSpawnTimers();
        }

        void Update()
        {
            if (!_isEnabled || waves.Length == 0)
            {
                return;
            }

            SpawnOnTimer();
        }

        private void ResetSpawnTimers()
        {
            if (waves.Length == 0)
            {
                return;
            }

            for (int i = 0; i < waves.Length; i++)
            {
                foreach (WaveSO.WaveDetail waveDetail in waves[i].waveDetails)
                {
                    waveDetail.nextSpawnTime = 0;
                }
            }
        }

        private bool ShouldMoveToNextWave()
        {
            return waves.Length > _waveIndex + 1 && timer.GetTime() > waves[_waveIndex + 1].spawnTimer;
        }

        private void SpawnOnTimer()
        {
            if (ShouldMoveToNextWave())
            {
                _waveIndex++;
            }

            foreach (WaveSO.WaveDetail waveDetail in waves[_waveIndex].waveDetails)
            {
                if (timer.GetTime() > waveDetail.nextSpawnTime)
                {
                    Spawn(waveDetail.enemyPrefab, waveDetail.distance, waveDetail.spawnCount);
                    waveDetail.nextSpawnTime = timer.GetTime() + waveDetail.spawnFrequency;
                    print($"Spawned {waveDetail.spawnCount} {waveDetail.enemyPrefab.name} enemy at {timer.GetTime():0.00}");
                }
            }
        }
        
        /// <summary>
        /// Spawns a given prefab at a certain distance from the center a given number of times.
        /// </summary>
        private void Spawn(GameObject prefab, float distance, int times)
        {
            for (int i = 0; i < times; i++)
            {
                // TODO: Implement object pooling
                Vector2 randomPosition = Vector2Extensions.GetRandomPositionByDistance(distance);
                GameObject enemy = Instantiate(prefab, new Vector3(randomPosition.x, randomPosition.y, 0),
                    Quaternion.identity, transform);

                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.ReadyEnemy();

                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemyMovement.Initialize(playerPosition);

                EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                enemyLoot.lootParent = lootParent;
            }
        }

        public void ResetEnemies()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < lootParent.childCount; i++)
            {
                Destroy(lootParent.GetChild(i).gameObject);
            }

            _waveIndex = 0;
            ResetSpawnTimers();
            _isEnabled = true;
        }

        public void PauseEnemyMovements()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                EnemyMovement enemyMovement = transform.GetChild(i).GetComponent<EnemyMovement>();
                enemyMovement.SetMovement(false);
                _pausedMovements.Add(enemyMovement);
            }            
        }

        public void ResumeEnemyMovements()
        {
            foreach (EnemyMovement enemyMovement in _pausedMovements)
            {
                enemyMovement.SetMovement(true);
            }
            
            _pausedMovements.Clear();
        }
    }
}