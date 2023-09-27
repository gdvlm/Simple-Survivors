using SimpleSurvivors.Player;
using UnityEngine;

namespace SimpleSurvivors.Item
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int healAmount = 5;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.HealPlayer(healAmount);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
