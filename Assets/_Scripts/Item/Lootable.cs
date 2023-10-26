using SimpleSurvivors.Player;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors.Item
{
    public class Lootable : MonoBehaviour
    {
        [SerializeField] private int experiencePoints;

        private SoundEffectManager _soundEffectManager;

        public void Initialize(SoundEffectManager soundEffectManager)
        {
            _soundEffectManager = soundEffectManager;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                PlayerExp playerExp = other.GetComponent<PlayerExp>();
                if (playerExp != null)
                {
                    _soundEffectManager.PlaySoundEffect(SoundEffect.ExpPickUp);
                    playerExp.GainExp(experiencePoints);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
