using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float speed = 1.0f;

    private Rigidbody2D _rigidbody2D;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (playerPosition.position - transform.position).normalized;
        _rigidbody2D.MovePosition(transform.position + direction * (speed * Time.fixedDeltaTime));
    }
}
