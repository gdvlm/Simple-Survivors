using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private GameObject lootPrefab;

        public void DropLoot()
        {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
    }
}