using System;
using System.Collections;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private float attackOffset = 1.0f;

        private readonly float _minimumDelay = 0.1f;
        private PlayerExp _playerExp;
        private GameObject _currentAttack;
        private bool _isAttacking;
        private int _attackDamage = 1;
        private float _attackDelay = 1.5f;

        void Awake()
        {
            _playerExp = GetComponent<PlayerExp>();
        }

        /// <summary>
        /// Create an attack prefab and attach to this object.
        /// </summary>
        private void InitializeAttack()
        {
            _currentAttack = Instantiate(attackPrefab, new Vector3(
                transform.position.x + attackOffset, 
                transform.position.y, 0), Quaternion.identity, transform);
            _currentAttack.SetActive(false);
        }

        private IEnumerator FireAttack()
        {
            while (_isAttacking)
            {
                // TODO: Refactor to use object pooling
                _currentAttack.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _currentAttack.SetActive(false);
                
                yield return new WaitForSeconds(Math.Max(_attackDelay, _minimumDelay));
            }
        }

        public void StartAttack()
        {
            _isAttacking = true;
            InitializeAttack();
            StartCoroutine(FireAttack());
        }

        /// <summary>
        /// Toggle whether the player is attacking.
        /// </summary>
        public void SetAttack(bool isAttacking)
        {
            _isAttacking = isAttacking;

            if (isAttacking)
            {
                StartCoroutine(FireAttack());
            }
        }

        /// <summary>
        /// Upgrades the attack given a percentage.
        /// </summary>
        public void UpgradeAttack(float percentage)
        {
            _attackDamage = (int)(_attackDamage * percentage);
        }

        /// <summary>
        /// Upgrades the attack delay by subtracting a static value.
        /// </summary>
        public void UpgradeAttackDelay(float delayPercentage)
        {
            if (_attackDelay <= _minimumDelay)
            {
                return;
            }
            
            _attackDelay -= delayPercentage;
        }

        public int GetAttackDamage()
        {
            return _attackDamage;
        }
    }
}