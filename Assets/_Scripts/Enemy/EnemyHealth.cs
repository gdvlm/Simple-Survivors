using SimpleSurvivors.Player;
using Unity.Mathematics;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySo;
        [SerializeField] private GameObject damagePrefab;
        
        private EnemyLoot _enemyLoot;
        private bool _isAlive;
        private int _enemyHp;
        private Transform _damagePopUpContainer;

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

                print($"Show damage: {playerAttack.GetAttackDamage()}");
                Instantiate(damagePrefab, transform.position, quaternion.identity, _damagePopUpContainer);

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

        public void ReadyEnemy(Transform damagePopupContainer)
        {
            _damagePopUpContainer = damagePopupContainer;
            _enemyHp = enemySo.enemyHp;
            _isAlive = true;
        }
    }
}