using SimpleSurvivors.Item;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private GameObject lootPrefab;
        
        private Transform _lootParent;
        private SoundEffectManager _soundEffectManager;

        public void Initialize(Transform lootParent, SoundEffectManager soundEffectManager)
        {
            _lootParent = lootParent;
            _soundEffectManager = soundEffectManager;
        }
        
        public void DropLoot()
        {
            var lootable = Instantiate(lootPrefab, transform.position, transform.rotation, _lootParent);
            lootable.GetComponent<Lootable>().Initialize(_soundEffectManager);
        }
    }
}