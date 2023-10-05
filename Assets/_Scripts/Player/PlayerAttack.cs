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
        [SerializeField] private GameObject spriteGo;

        private readonly float _minimumDelay = 0.1f;
        private int _startingAttackDamage;
        private float _startingAttackDelay;
        private GameObject _currentAttack;
        private bool _isAttacking;

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
                    transform.position.y + attackYOffset, 0), Quaternion.identity, transform);
            }
            
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

        public void SetPlayerDirection(float newYRotation)
        {
            spriteGo.transform.eulerAngles = new(spriteGo.transform.eulerAngles.x, newYRotation,
                spriteGo.transform.eulerAngles.z);

            float xOffset = newYRotation > 0.0f ? attackXOffset : -attackXOffset;
            _currentAttack.transform.position =
                new Vector3(transform.position.x + xOffset, transform.position.y + attackYOffset, 0);

            float reverseAngle = newYRotation == 0 ? 180.0f : 0f;
            _currentAttack.transform.eulerAngles = new(_currentAttack.transform.eulerAngles.x,
                reverseAngle, _currentAttack.transform.eulerAngles.z);
        }
    }
}