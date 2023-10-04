using SimpleSurvivors.Player;
using TMPro;
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
                
                var prefab = Instantiate(damagePrefab, transform.position,
                    quaternion.identity, _damagePopUpContainer);
                TMP_Text damageText = prefab.GetComponentInChildren<TMP_Text>();
                damageText.text = playerAttack.GetAttackDamage().ToString();

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
            gameObject.SetActive(false);
        }

        public void ReadyEnemy(Transform damagePopupContainer, EnemySpawner enemySpawner)
        {
            _damagePopUpContainer = damagePopupContainer;
            _enemyHp = enemySo.enemyHp;
            _isAlive = true;
        }
    }
}