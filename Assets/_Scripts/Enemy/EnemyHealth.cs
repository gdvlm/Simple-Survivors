using SimpleSurvivors.Player;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySo;
        
        private EnemyLoot _enemyLoot;
        private bool _isAlive;
        private int _enemyHp;

        void Awake()
        {
            _enemyLoot = GetComponent<EnemyLoot>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (_isAlive && other.transform.CompareTag("PlayerAttack"))
            {
                var playerAttack = other.GetComponentInParent<PlayerAttack>();
                _enemyHp -= playerAttack.GetAttackDamage();

                if (_enemyHp <= 0)
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
            _enemyHp = enemySo.enemyHp;
            _isAlive = true;
        }
    }
}