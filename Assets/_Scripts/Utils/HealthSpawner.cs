using SimpleSurvivors.Extensions;
using SimpleSurvivors.Item;
using UnityEngine;

namespace SimpleSurvivors.Utils
{
    public class HealthSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private Timer timer;
        [SerializeField] private SoundEffectManager soundEffectManager;
        
        [SerializeField][Tooltip("Spawn cooldown in seconds")]
        private float spawnCoolDown = 15.0f;
        [SerializeField][Tooltip("Maximum distance from the center of the map to spawn the prefabs")]
        private float spawnMaxRadius = 8.0f;
        [SerializeField][Tooltip("Minimum distance from the center of the map to spawn the prefabs")]
        private float spawnMinRadius = 3.0f;        

        private bool _isEnabled;
        private float _remainingTime;

        void Start()
        {
            SetSpawnTimer();
        }

        void Update()
        {
            if (!_isEnabled)
            {
                return;
            }
            
            if (timer.GetTime() >= _remainingTime)
            {
                SpawnHealth();
                SetSpawnTimer();
            }
        }

        private void SpawnHealth()
        {
            float randomDistance = Random.Range(spawnMinRadius, spawnMaxRadius);
            Vector2 randomPosition = Vector2Extensions.GetRandomPositionByDistance(randomDistance);

            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject health = Instantiate(prefabs[randomIndex], new Vector3(randomPosition.x, 
                randomPosition.y, 0), Quaternion.identity, transform);
            health.GetComponent<Health>().Initialize(soundEffectManager);
        }

        public void SetSpawnTimer()
        {
            _remainingTime = timer.GetTime() + spawnCoolDown;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }
    }
}
