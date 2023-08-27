using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D _rigidbody2D;
    private PlayerHealth _playerHealth;
    private PlayerInputActionWrapper _playerInputActionWrapper;
    private Vector2 _velocity;

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
        if (_playerHealth.IsAlive())
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
}