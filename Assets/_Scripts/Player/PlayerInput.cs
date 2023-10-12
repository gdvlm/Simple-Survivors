using SimpleSurvivors.InputActionWrappers;
using SimpleSurvivors.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleSurvivors.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private GameObject pauseMenuCanvas;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlayerDirectionVariable playerDirectionVariable;

        private readonly float _maximumMovementSpeed = 6f;
        private float _startingMovementSpeed;
        private Rigidbody2D _rigidbody2D;
        private Player _player;
        private PlayerAttack _playerAttack;
        private PlayerInputActionWrapper _playerInputActionWrapper;
        private Vector2 _velocity;
        private bool _canMove;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _player = GetComponent<Player>();
            _playerAttack = GetComponent<PlayerAttack>();
            _playerInputActionWrapper = new PlayerInputActionWrapper();
            _playerInputActionWrapper.Gameplay.Pause.performed += OnPause;
        }

        void Start()
        {
            _startingMovementSpeed = movementSpeed;
        }

        void Update()
        {
            _velocity = _playerInputActionWrapper.Gameplay.Movement.ReadValue<Vector2>();

            if (!PlayerIsFacingSameDirection(_velocity) && _canMove)
            {
                float newYRotation = playerDirectionVariable.RuntimeValue == PlayerDirection.Left
                    ? 0
                    : 180;
                _playerAttack.SetPlayerDirection(newYRotation);
            }
        }

        void FixedUpdate()
        {
            if (_player.IsAlive() && _canMove)
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
            if (playerDirection == playerDirectionVariable.RuntimeValue)
            {
                return true;
            }

            playerDirectionVariable.RuntimeValue = playerDirection;
            return false;
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            // Handle resuming
            if (pauseMenuCanvas.activeSelf)
            {
                gameManager.ResumeGame();
                return;
            }
            
            // Ignore input
            if (!_canMove)
            {
                return;
            }
            
            pauseMenuCanvas.SetActive(true);
            gameManager.PauseGame();
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