using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject playerHpSprite;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private Timer timer;

    private bool _isAlive;
    private int _playerMaxHp;
    private int _playerCurrentHp;
    private Vector3 _startingPosition;
    
    void Start()
    {
        _startingPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            int damage = 50;
            TakeDamage(damage);
        }
    }

    private void TakeDamage(int damage)
    {
        if (_playerCurrentHp == 0)
        {
            return;
        }
        
        _playerCurrentHp = Math.Clamp(_playerCurrentHp, 0, _playerCurrentHp - damage);
        
        float hpPercent = (float)_playerCurrentHp / _playerMaxHp;
        playerHpSprite.transform.localScale = new Vector3(hpPercent, 1, 1);

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
        timer.PauseTimer();
    }

    public void ReadyPlayer()
    {
        _isAlive = true;
        _playerMaxHp = 100;
        _playerCurrentHp = _playerMaxHp;
        playerSprite.SetActive(true);
        transform.position = _startingPosition;
        playerHpSprite.transform.localScale = Vector3.one;
    }

    public bool IsAlive()
    {
        return _isAlive;
    }
}
