using System;
using SimpleSurvivors.Enemy;
using SimpleSurvivors.Utils;
using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject playerSprite;
        [SerializeField] private GameObject defeatCanvas;
        [SerializeField] private Timer timer;
        [SerializeField] private IntVariable maxPlayerHp;
        [SerializeField] private IntVariable currentPlayerHp;
        [SerializeField] private IntVariable enemyAttack;
        [SerializeField] private EnemySpawner enemySpawner;

        private PlayerAttack _playerAttack;
        private PlayerExp _playerExp;
        private PlayerInput _playerInput;
        private bool _isAlive;
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
                TakeDamage(enemyAttack.RuntimeValue);
            }
        }

        private void TakeDamage(int damage)
        {
            if (currentPlayerHp.RuntimeValue == 0)
            {
                return;
            }
        
            currentPlayerHp.RuntimeValue = Math.Max(0, currentPlayerHp.RuntimeValue - damage);
            if (currentPlayerHp.RuntimeValue == 0)
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
            _playerAttack.SetAttack(false);
            enemySpawner.PauseEnemyMovements();
        }

        public void ReadyPlayer()
        {
            _isAlive = true;
            currentPlayerHp.RuntimeValue = maxPlayerHp.RuntimeValue;
            playerSprite.SetActive(true);
            transform.position = _startingPosition;
            _playerExp.ResetLevel();
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
            maxPlayerHp.RuntimeValue = (int)(maxPlayerHp.RuntimeValue * percentage);
        }
    }
}