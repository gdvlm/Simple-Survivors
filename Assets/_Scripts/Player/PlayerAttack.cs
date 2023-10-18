using System.Collections;
using SimpleSurvivors.Utils;
using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private Transform playerSprite;
        [SerializeField] private SoundEffectManager soundEffectManager;
        [SerializeField] private float attackXOffset = -1.0f;
        [SerializeField] private float attackYOffset = -0.5f;
        [SerializeField] private FloatVariable attackDelay;
        [SerializeField] private FloatVariable animationDelay;
        [SerializeField] private IntVariable attackDamage;
        
        private GameObject _currentAttack;
        private bool _isAttacking;
        private float _lastYRotation = 180f;

        /// <summary>
        /// Create an attack prefab and attach to this object.
        /// </summary>
        private void Initialize()
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
            attackDamage.RuntimeValue = attackDamage.InitialValue;
        }

        private IEnumerator FireAttack()
        {
            while (_isAttacking)
            {
                SetAttackPositionAndRotation();

                _currentAttack.SetActive(true);
                soundEffectManager.PlaySoundEffect(SoundEffect.Attack);
                yield return new WaitForSeconds(animationDelay.RuntimeValue);

                _currentAttack.SetActive(false);
                yield return new WaitForSeconds(attackDelay.RuntimeValue);
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
            Initialize();
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

        public void SetPlayerDirection(float newYRotation)
        {
            _lastYRotation = newYRotation;
            playerSprite.eulerAngles = new(
                playerSprite.eulerAngles.x,
                newYRotation,
                playerSprite.eulerAngles.z);
        }
    }
}