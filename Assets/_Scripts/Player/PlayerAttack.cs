using System;
using System.Collections;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private float attackXOffset = -1.0f;
        [SerializeField] private float attackYOffset = -0.5f;
        [SerializeField] private int attackDamage = 1;
        [SerializeField] private float attackDelay = 1.5f;
        [SerializeField] private float animationDelay = 0.5f;
        [SerializeField] private GameObject playerSprite;

        private readonly float _minimumDelay = 0.1f;
        private int _startingAttackDamage;
        private float _startingAttackDelay;
        private GameObject _currentAttack;
        private bool _isAttacking;
        private float _lastYRotation = 180f;

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
            if (_currentAttack == null)
            {
                _currentAttack = Instantiate(attackPrefab, new Vector3(
                    transform.position.x + attackXOffset, 
                    transform.position.y + attackYOffset, 0), Quaternion.identity);
                
                // Set animation speed
                var animator = _currentAttack.GetComponentInChildren<Animator>();
                animator.speed /= animationDelay;
            }
            
            _currentAttack.SetActive(false);
            attackDamage = _startingAttackDamage;
            attackDelay = _startingAttackDelay;
        }

        private IEnumerator FireAttack()
        {
            while (_isAttacking)
            {
                SetAttackPositionAndRotation();

                _currentAttack.SetActive(true);
                yield return new WaitForSeconds(animationDelay);
                
                _currentAttack.SetActive(false);
                yield return new WaitForSeconds(Math.Max(attackDelay, _minimumDelay));
            }
        }

        private void SetAttackPositionAndRotation()
        {
            float xOffset = _lastYRotation > 0.0f ? attackXOffset : -attackXOffset;
            _currentAttack.transform.position =
                new Vector3(transform.position.x + xOffset, transform.position.y + attackYOffset, 0);

            float reverseAngle = _lastYRotation == 0 ? 180.0f : 0f;
            _currentAttack.transform.eulerAngles = new(
                _currentAttack.transform.eulerAngles.x,
                reverseAngle,
                _currentAttack.transform.eulerAngles.z);
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

            if (!isAttacking)
            {
                StopAllCoroutines();
                return;
            }

            StartCoroutine(FireAttack());
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

        public void SetPlayerDirection(float newYRotation)
        {
            _lastYRotation = newYRotation;
            playerSprite.transform.eulerAngles = new(
                playerSprite.transform.eulerAngles.x,
                newYRotation,
                playerSprite.transform.eulerAngles.z);
        }
    }
}