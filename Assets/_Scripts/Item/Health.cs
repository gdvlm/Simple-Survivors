using System;
using SimpleSurvivors.Utils;
using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Item
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int healAmount = 5;
        [SerializeField] private IntVariable maxPlayerHp;
        [SerializeField] private IntVariable currentPlayerHp;
        
        private SoundEffectManager _soundEffectManager;

        public void Initialize(SoundEffectManager soundEffectManager)
        {
            _soundEffectManager = soundEffectManager;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                Player.Player player = other.GetComponent<Player.Player>();
                if (player != null)
                {
                    if (currentPlayerHp.RuntimeValue == maxPlayerHp.RuntimeValue)
                    {
                        return;
                    }
            
                    _soundEffectManager.PlaySoundEffect(SoundEffect.HealthPickUp);
                    currentPlayerHp.RuntimeValue = Math.Min(currentPlayerHp.RuntimeValue + healAmount, maxPlayerHp.RuntimeValue);                    
                }
                
                Destroy(gameObject);
            }
        }
    }
}
