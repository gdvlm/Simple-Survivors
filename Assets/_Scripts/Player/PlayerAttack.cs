using System;
using System.Collections;
using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private GameObject playerSprite;
        [SerializeField] private float attackXOffset = -1.0f;
        [SerializeField] private float attackYOffset = -0.5f;
        [SerializeField] private FloatVariable attackDelay;
        [SerializeField] private FloatVariable animationDelay;
        [SerializeField] private IntVariable attackDamage;

        private readonly float _minimumDelay = 0.1f;
        private GameObject _currentAttack;
        private bool _isAttacking;
        private float _lastYRotation = 180f;

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
            }

            SetAttackAnimationSpeed(1 / animationDelay.RuntimeValue);
            _currentAttack.SetActive(false);
            attackDelay.RuntimeValue = attackDelay.InitialValue;
        }

        private IEnumerator FireAttack()
        {
            while (_isAttacking)
            {
                SetAttackPositionAndRotation();

                _currentAttack.SetActive(true);
                yield return new WaitForSeconds(animationDelay.RuntimeValue);

                _currentAttack.SetActive(false);
                yield return new WaitForSeconds(Math.Max(attackDelay.RuntimeValue, _minimumDelay));
            }
        }

        /// <summary>
        /// Set current attack's animation speed.
        /// </summary>
        private void SetAttackAnimationSpeed(float speed)
        {
            var animator = _currentAttack.GetComponentInChildren<Animator>();
            animator.speed = speed;
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
                SetAttackAnimationSpeed(0);
                StopAllCoroutines();
                return;
            }

            SetAttackAnimationSpeed(1 / animationDelay.RuntimeValue);
            StartCoroutine(FireAttack());
        }

        /// <summary>
        /// Upgrades the attack given a percentage.
        /// </summary>
        public void UpgradeAttack(float percentage)
        {
            attackDamage.RuntimeValue = (int)(attackDamage.RuntimeValue * percentage);
        }

        /// <summary>
        /// Upgrades the attack delay by subtracting a static value.
        /// </summary>
        public void UpgradeAttackDelay(float delayValue)
        {
            if (attackDelay.RuntimeValue <= _minimumDelay)
            {
                return;
            }

            attackDelay.RuntimeValue -= delayValue;
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