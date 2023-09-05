using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        // TODO: Integrate with Scriptable Objects
        private bool _isAlive = false;
        public int _enemyHp;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_isAlive && other.transform.CompareTag("PlayerAttack"))
            {
                _enemyHp--;

                if (_enemyHp == 0)
                {
                    _isAlive = false;
                    Destroy(gameObject);
                }
            }
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