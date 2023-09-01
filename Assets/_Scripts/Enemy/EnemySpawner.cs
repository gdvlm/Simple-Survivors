using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private GameObject purpleBallPrefab;

    private List<GameObject> _enemies = new();
    private List<EnemyMovement> _enemyMovements = new();

    /// <summary>
    /// Get a random position based on a certain distance from the center.
    /// </summary>
    private Vector3 GetRandomPositionByDistance(float distance)
    {
        return new Vector3(-0.06f, -4f, 0);
    }

    /// <summary>
    /// Spawns a given prefab at a certain distance from the center a given number of times.
    /// </summary>
    private void Spawn(GameObject prefab, float distance, int times)
    {
        for (int i = 0; i < times; i++)
        {
            // TODO: Implement object pooling
            Vector3 randomPosition = GetRandomPositionByDistance(distance);
            GameObject enemy = Instantiate(prefab, randomPosition, Quaternion.identity, enemyParent);

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.Initialize(playerPosition);
            _enemyMovements.Add(enemyMovement);
            _enemies.Add(enemy);            
        }
    }

    public void SpawnWave1()
    {
        Spawn(purpleBallPrefab, 3.0f, 1);
    }
}
