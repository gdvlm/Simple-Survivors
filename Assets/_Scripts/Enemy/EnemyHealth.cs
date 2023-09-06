using System;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        // TODO: Integrate with Scriptable Objects
        private EnemyLoot _enemyLoot;
        private bool _isAlive = false;
        public int _enemyHp;

        void Awake()
        {
            _enemyLoot = GetComponent<EnemyLoot>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_isAlive && other.transform.CompareTag("PlayerAttack"))
            {
                _enemyHp--;

                if (_enemyHp == 0)
                {
                    KillEnemy();
                }
            }
        }

        private void KillEnemy()
        {
            _isAlive = false;
            _enemyLoot.DropLoot();
            
            Destroy(gameObject);
        }

        public void ReadyEnemy()
        {
            _isAlive = true;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }
    }
}