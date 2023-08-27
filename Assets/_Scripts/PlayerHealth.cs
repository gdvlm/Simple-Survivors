using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerHpSprite;

    private int _playerMaxHp;
    private int _playerCurrentHp;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerMaxHp = 100;
        _playerCurrentHp = _playerMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.transform.name);
    }
}
