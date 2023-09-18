using UnityEngine;

namespace SimpleSurvivors.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        private readonly float _maximumMovementSpeed = 6f;
        private Rigidbody2D _rigidbody2D;
        private PlayerHealth _playerHealth;
        private PlayerInputActionWrapper _playerInputActionWrapper;
        private Vector2 _velocity;
        private bool _canMove;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerInputActionWrapper = new PlayerInputActionWrapper();
        }

        void Update()
        {
            _velocity = _playerInputActionWrapper.Gameplay.Movement.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            if (_playerHealth.IsAlive() && _canMove)
            {
                _rigidbody2D.MovePosition(_rigidbody2D.position + _velocity * (movementSpeed * Time.fixedDeltaTime));
            }
        }

        void OnEnable()
        {
            _playerInputActionWrapper.Gameplay.Enable();
        }

        void OnDisable()
        {
            _playerInputActionWrapper.Gameplay.Disable();
        }
        
        /// <summary>
        /// Toggle whether the player can move.
        /// </summary>
        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;
        }

        /// <summary>
        /// Upgrades the movement speed by a percentage.
        /// </summary>
        public void UpgradeMovementSpeed(float percentage)
        {
            if (movementSpeed >= _maximumMovementSpeed)
            {
                return;
            }
            
            movementSpeed *= percentage;
        }
    }
}