using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerHpSprite;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject defeatCanvas;

    private bool _isAlive;
    private int _playerMaxHp;
    private int _playerCurrentHp;
    private Vector3 _startingPosition;
    
    void Start()
    {
        _startingPosition = transform.position;
        ReadyPlayer();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            int damage = 50;
            TakeDamage(damage);
        }
    }

    private void TakeDamage(int damage)
    {
        _playerCurrentHp = Math.Clamp(_playerCurrentHp, 0, _playerCurrentHp - damage);
        
        // TODO: Update player hp sprite to reflect current HP
        print(_playerCurrentHp);

        if (_playerCurrentHp == 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        _isAlive = false;
        playerSprite.SetActive(false);
        defeatCanvas.SetActive(true);
    }

    public void ReadyPlayer()
    {
        _isAlive = true;
        _playerMaxHp = 100;
        _playerCurrentHp = _playerMaxHp;
        playerSprite.SetActive(true);
        transform.position = _startingPosition;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }
}
