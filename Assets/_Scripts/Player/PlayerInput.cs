using UnityEngine;

namespace SimpleSurvivors.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        private readonly float _maximumMovementSpeed = 6f;
        private float _startingMovementSpeed;
        private Rigidbody2D _rigidbody2D;
        private PlayerHealth _playerHealth;
        private PlayerAttack _playerAttack;
        private PlayerInputActionWrapper _playerInputActionWrapper;
        private Vector2 _velocity;
        private bool _canMove;
        private PlayerDirection _playerDirection;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerAttack = GetComponent<PlayerAttack>();
            _playerInputActionWrapper = new PlayerInputActionWrapper();
        }

        void Start()
        {
            _startingMovementSpeed = movementSpeed;
            _playerDirection = PlayerDirection.Right;
        }

        void Update()
        {
            _velocity = _playerInputActionWrapper.Gameplay.Movement.ReadValue<Vector2>();

            if (!PlayerIsFacingSameDirection(_velocity))
            {
                float newYRotation = _playerDirection == PlayerDirection.Left
                    ? 0
                    : 180;
                _playerAttack.SetPlayerDirection(newYRotation);
            }
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
        /// Indicates whether the player is facing a new direction compared to previous frame.
        /// </summary>
        private bool PlayerIsFacingSameDirection(Vector2 input)
        {
            if (input.x == 0)
            {
                return true;
            }
            
            PlayerDirection playerDirection = input.x > 0
                ? PlayerDirection.Right
                : PlayerDirection.Left;
            if (playerDirection == _playerDirection)
            {
                return true;
            }

            _playerDirection = playerDirection;
            return false;
        }
        
        /// <summary>
        /// Toggle whether the player can move.
        /// </summary>
        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;
        }

        public void ResetMovementSpeed()
        {
            movementSpeed = _startingMovementSpeed;
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