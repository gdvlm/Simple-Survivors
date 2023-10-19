using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private GameObject lootPrefab;

        [HideInInspector] public Transform lootParent;

        public void DropLoot()
        {
            Instantiate(lootPrefab, transform.position, transform.rotation, lootParent);
        }
    }
}