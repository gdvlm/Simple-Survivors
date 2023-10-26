using System.Collections.Generic;
using System.Linq;
using SimpleSurvivors.Extensions;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform damagePopupContainer;
        [SerializeField] private Timer timer;
        [SerializeField] private Transform lootParent;
        [SerializeField] private SoundEffectManager soundEffectManager;
        [SerializeField] private WaveSO[] waves;

        private readonly Dictionary<string, List<GameObject>> _pooledEnemyHash = new();
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
                }
            }
        }

        /// <summary>
        ///     Spawns a given prefab at a certain distance from the center a given number of times.
        /// </summary>
        private void Spawn(GameObject prefab, float distance, int times)
        {
            for (int i = 0; i < times; i++)
            {
                Vector2 randomPosition = Vector2Extensions.GetRandomPositionByDistance(distance);
                GameObject enemy = GetOrCreate(prefab);
                enemy.transform.position = randomPosition;

                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                enemyHealth.Initialize(damagePopupContainer);

                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                enemyMovement.Initialize(playerPosition);

                EnemyLoot enemyLoot = enemy.GetComponent<EnemyLoot>();
                enemyLoot.Initialize(lootParent, soundEffectManager);
            }
        }

        /// <summary>
        ///     Retrive an enemy using object pooling.
        /// </summary>
        private GameObject GetOrCreate(GameObject prefab)
        {
            if (_pooledEnemyHash.ContainsKey(prefab.name) && _pooledEnemyHash[prefab.name].Any(pe => !pe.activeSelf))
            {
                GameObject pooledEnemy = _pooledEnemyHash[prefab.name].First(pe => !pe.activeSelf);
                pooledEnemy.SetActive(true);
                return pooledEnemy;
            }

            GameObject enemy = Instantiate(prefab, transform);
            if (_pooledEnemyHash.ContainsKey(prefab.name))
            {
                _pooledEnemyHash[prefab.name].Add(enemy);
                return enemy;
            }

            _pooledEnemyHash[prefab.name] = new() { enemy };
            return enemy;
        }

        public void ResetEnemies()
        {
            // Re-enable animation on enemies paused upon player death
            ResumeEnemyMovements();

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
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
                Transform child = transform.GetChild(i);
                if (!child.gameObject.activeSelf)
                {
                    continue;
                }

                EnemyMovement enemyMovement = child.GetComponent<EnemyMovement>();
                enemyMovement.SetMovement(false);
                _pausedMovements.Add(enemyMovement);

                Animator animator = child.GetComponentInChildren<Animator>();
                animator.speed = 0f;
            }
        }

        public void ResumeEnemyMovements()
        {
            foreach (EnemyMovement enemyMovement in _pausedMovements)
            {
                enemyMovement.SetMovement(true);

                Animator animator = enemyMovement.GetComponentInChildren<Animator>();
                animator.speed = 1;
            }

            _pausedMovements.Clear();
        }
    }
}