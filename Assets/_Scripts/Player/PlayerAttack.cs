using System;
using System.Collections;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private float attackOffset = 1.0f;
        [SerializeField] private int attackDamage = 1;
        [SerializeField] private float attackDelay = 1.5f;

        private readonly float _minimumDelay = 0.1f;
        private int _startingAttackDamage;
        private float _startingAttackDelay;
        private PlayerExp _playerExp;
        private GameObject _currentAttack;
        private bool _isAttacking;

        void Awake()
        {
            _playerExp = GetComponent<PlayerExp>();
        }

        private void Start()
        {
            _startingAttackDamage = attackDamage;
            _startingAttackDelay = attackDelay;
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
            attackDamage = _startingAttackDamage;
            attackDelay = _startingAttackDelay;
        }

        private IEnumerator FireAttack()
        {
            while (_isAttacking)
            {
                // TODO: Refactor to use object pooling
                _currentAttack.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _currentAttack.SetActive(false);
                
                yield return new WaitForSeconds(Math.Max(attackDelay, _minimumDelay));
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
            attackDamage = (int)(attackDamage * percentage);
        }

        /// <summary>
        /// Upgrades the attack delay by subtracting a static value.
        /// </summary>
        public void UpgradeAttackDelay(float delayValue)
        {
            if (attackDelay <= _minimumDelay)
            {
                return;
            }
            
            attackDelay -= delayValue;
        }

        public int GetAttackDamage()
        {
            return attackDamage;
        }
    }
}