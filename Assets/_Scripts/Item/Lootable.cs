using UnityEngine;

namespace SimpleSurvivors.Item
{
    public class Lootable : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                print("Exp + 1!!!");
                Destroy(gameObject);
            }
        }
    }
}
