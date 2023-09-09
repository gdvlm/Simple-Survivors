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
        [SerializeField] private SpawnSchedule[] spawnSchedules;

        private List<GameObject> _enemies = new();
        private bool _isEnabled = false;
        private int _currentScheduleIndex = 0;

        void Update()
        {
            if (!_isEnabled || spawnSchedules.Length == 0)
            {
                return;
            }

            SpawnOnSchedule();
        }

        private void SpawnOnSchedule()
        {
            if (spawnSchedules[_currentScheduleIndex].time < timer.GetTime())
            {
                Spawn(spawnSchedules[_currentScheduleIndex].prefab,
                    spawnSchedules[_currentScheduleIndex].distance,
                    spawnSchedules[_currentScheduleIndex].count);
                _currentScheduleIndex++;

                if (_currentScheduleIndex == spawnSchedules.Length)
                {
                    _isEnabled = false;
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
                _enemies.Add(enemy);

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

            _currentScheduleIndex = 0;
            _isEnabled = true;
        }
    }
}