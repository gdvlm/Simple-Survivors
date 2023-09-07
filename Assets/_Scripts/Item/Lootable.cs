using SimpleSurvivors.Player;
using UnityEngine;

namespace SimpleSurvivors.Item
{
    public class Lootable : MonoBehaviour
    {
        [SerializeField] private int experiencePoints;
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                PlayerExp playerExp = other.GetComponent<PlayerExp>();
                if (playerExp != null)
                {
                    playerExp.GainExp(experiencePoints);
                }
                
                Destroy(gameObject);
            }
        }
    }
}
