using System;
using SimpleSurvivors.Enemy;
using SimpleSurvivors.Utils;
using SimpleSurvivors.Variables;
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
        [SerializeField] private IntVariable playerHp;
        [SerializeField] private int enemyAttack = 40;
        [SerializeField] private EnemySpawner enemySpawner;

        private PlayerAttack _playerAttack;
        private PlayerExp _playerExp;
        private PlayerInput _playerInput;
        private bool _isAlive;
        private int _playerCurrentHp;
        private Vector3 _startingPosition;
        private CapsuleCollider2D _capsuleCollider;

        void Awake()
        {
            _playerAttack = GetComponentInChildren<PlayerAttack>();
            _playerExp = GetComponent<PlayerExp>();
            _playerInput = GetComponent<PlayerInput>();
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
                TakeDamage(enemyAttack);
            }
        }

        private void TakeDamage(int damage)
        {
            if (_playerCurrentHp == 0)
            {
                return;
            }
        
            _playerCurrentHp = Math.Max(0, _playerCurrentHp - damage);
            UpdateHealthBar();

            if (_playerCurrentHp == 0)
            {
                KillPlayer();
            }
        }

        private void UpdateHealthBar()
        {
            float hpPercent = (float)_playerCurrentHp / playerHp.RuntimeValue;
            playerHpSprite.transform.localScale = new Vector3(hpPercent, 1, 1);            
        }

        private void KillPlayer()
        {
            _isAlive = false;
            playerSprite.SetActive(false);
            defeatCanvas.SetActive(true);
            timer.PauseTimer();
            _playerAttack.SetAttack(false);
            enemySpawner.PauseEnemyMovements();
        }

        public void HealPlayer(int healAmount)
        {
            if (_playerCurrentHp == playerHp.RuntimeValue)
            {
                return;
            }
            
            _playerCurrentHp = Math.Min(_playerCurrentHp + healAmount, playerHp.RuntimeValue);
            UpdateHealthBar();
        }

        public void ReadyPlayer()
        {
            // TODO: Refactor to PlayerController class
            _isAlive = true;
            _playerCurrentHp = playerHp.RuntimeValue;
            playerSprite.SetActive(true);
            transform.position = _startingPosition;
            playerHpSprite.transform.localScale = Vector3.one;
            _playerExp.ResetExp();
            _playerAttack.StartAttack();
            _playerInput.ResetMovementSpeed();
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
        
        /// <summary>
        /// Upgrades the health given a percentage.
        /// </summary>
        public void UpgradeHealth(float percentage)
        {
            playerHp.RuntimeValue = (int)(playerHp.RuntimeValue * percentage);
            UpdateHealthBar();
        }
    }
}