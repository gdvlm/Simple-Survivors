using SimpleSurvivors.Extensions;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;

        private Rigidbody2D _rigidbody2D;
        private Transform _playerPosition;
        private Player.Player _player;
        private bool _isMoving;
    
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            // _playerHealth is null until Initialize() is called
            if (_player?.IsAlive() == true && _isMoving)
            {
                MoveTowardsPlayer();    
            }
        }

        private void MoveTowardsPlayer()
        {
            Vector3 direction = (_playerPosition.position - transform.position).normalized;
            _rigidbody2D.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
        }

        public void Initialize(Transform playerPosition)
        {
            _playerPosition = playerPosition;
            _player = playerPosition.GetComponent<Player.Player>();
            _isMoving = true;
        }

        public void ResetRandomPosition()
        {
            float distance = 8.0f;
            Vector2 randomPosition = Vector2Extensions.GetRandomPositionByDistance(distance);
            transform.position = new Vector3(randomPosition.x, randomPosition.y, 1);
        }

        /// <summary>
        /// Toggles whether the enemy is moving.
        /// </summary>
        public void SetMovement(bool isMoving)
        {
            _isMoving = isMoving;
        }
    }
}