using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private Vector2 GetRandomPositionByDistance(float distance)
    {
        return Random.insideUnitCircle.normalized * distance;
    }

    /// <summary>
    /// Spawns a given prefab at a certain distance from the center a given number of times.
    /// </summary>
    private void Spawn(GameObject prefab, float distance, int times)
    {
        for (int i = 0; i < times; i++)
        {
            // TODO: Implement object pooling
            Vector2 randomPosition = GetRandomPositionByDistance(distance);
            GameObject enemy = Instantiate(prefab, new Vector3(randomPosition.x, randomPosition.y, 0),
                Quaternion.identity, enemyParent);

            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            enemyMovement.Initialize(playerPosition);
            _enemyMovements.Add(enemyMovement);
            _enemies.Add(enemy);            
        }
    }

    public void SpawnWave1()
    {
        Spawn(purpleBallPrefab, 8.0f, 50);
    }
}
