using System;
using System.Collections;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private float attackOffset = 1.0f;

        private PlayerExp _playerExp;
        private GameObject _currentAttack;
        private bool _isRunning;

        void Awake()
        {
            _playerExp = GetComponent<PlayerExp>();
        }

        /// <summary>
        /// Create an attack prefab and attach to this object.
        /// </summary>
        private void InitializeAttack()
        {
            _currentAttack = Instantiate(attackPrefab, new Vector3(transform.position.x + attackOffset, 
                transform.position.y, 0), Quaternion.identity, transform);
            _currentAttack.SetActive(false);
        }

        private IEnumerator FireAttack()
        {
            while (_isRunning)
            {
                // TODO: Refactor to use object pooling
                _currentAttack.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _currentAttack.SetActive(false);

                // Delay
                // TODO: Replace with actual level up mechanism
                float baseDelay = 1.5f;
                float levelBonus = (float)_playerExp.GetLevel() / 100 * baseDelay;
                yield return new WaitForSeconds(Math.Clamp(baseDelay - levelBonus, 0.1f, baseDelay));
            }
        }

        public void StartAttack()
        {
            _isRunning = true;
            InitializeAttack();
            StartCoroutine(FireAttack());
        }

        public void StopAttack()
        {
            _isRunning = false;
        }
    }
}