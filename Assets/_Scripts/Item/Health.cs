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
                Player.Player player = other.GetComponent<Player.Player>();
                if (player != null)
                {
                    player.HealPlayer(healAmount);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
