using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Timer timer;
    [SerializeField] private SpawnSchedule[] spawnSchedules;

    private List<GameObject> _enemies = new();
    private List<EnemyMovement> _enemyMovements = new();
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

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.Initialize(playerPosition);
            _enemyMovements.Add(enemyMovement);
            _enemies.Add(enemy);            
        }
    }

    public void ResetEnemies()
    {
        foreach (EnemyMovement enemyMovement in _enemyMovements)
        {
            enemyMovement.ResetRandomPosition();
        }

        _currentScheduleIndex = 0;
        _isEnabled = true;
    }
}
