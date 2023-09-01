using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private Rigidbody2D _rigidbody2D;
    private Transform _playerPosition;
    private PlayerHealth _playerHealth;
    
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // _playerHealth is null until Initialize() is called
        if (_playerHealth?.IsAlive() == true)
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
        _playerHealth = playerPosition.GetComponent<PlayerHealth>();
    }

    public void ResetRandomPosition()
    {
        // TODO: Get these values dynamically
        float screenMinX = -9;
        float screenMaxX = 9;
        float screenMinY = -9;
        float screenMaxY = 9;
        int offset = 1;
        float randomX = Random.Range(screenMinX + offset, screenMaxX - offset);
        float randomY = Random.Range(screenMinY + offset, screenMaxY - offset);
        transform.position = new Vector3(randomX, randomY, 1);
    }
}
