using System;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject playerHpSprite;
        [SerializeField] private GameObject playerSprite;
        [SerializeField] private GameObject defeatCanvas;
        [SerializeField] private Timer timer;
        [SerializeField][Tooltip("Override the player HP for testing.")] private int playerHp;

        private PlayerAttack _playerAttack;
        private PlayerExp _playerExp;
        private bool _isAlive;
        private int _playerMaxHp;
        private int _playerCurrentHp;
        private Vector3 _startingPosition;
        private CapsuleCollider2D _capsuleCollider;

        void Awake()
        {
            _playerAttack = GetComponentInChildren<PlayerAttack>();
            _playerExp = GetComponent<PlayerExp>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        void Start()
        {
            _startingPosition = transform.position;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_capsuleCollider.IsTouching(other) && other.transform.CompareTag("Enemy"))
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
            _playerAttack.StopAttack();
        }

        public void ReadyPlayer()
        {
            // TODO: Refactor to PlayerController class
            _isAlive = true;
            _playerMaxHp = playerHp;
            _playerCurrentHp = _playerMaxHp;
            playerSprite.SetActive(true);
            transform.position = _startingPosition;
            playerHpSprite.transform.localScale = Vector3.one;
            _playerExp.ResetExp();
            _playerAttack.StartAttack();
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
    }
}