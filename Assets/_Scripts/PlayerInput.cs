using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody2D _rigidbody2D;
    private PlayerInputActionWrapper _playerInputActionWrapper;
    private Vector2 _velocity;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInputActionWrapper = new PlayerInputActionWrapper();
    }

    void Update()
    {
        _velocity = _playerInputActionWrapper.Gameplay.Movement.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _velocity * (movementSpeed * Time.fixedDeltaTime));
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