using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject playerHpSprite;
        [SerializeField] private IntVariable maxPlayerHp;
        [SerializeField] private IntVariable currentPlayerHp;

        // Update is called once per frame
        void Update()
        {
            float hpPercent = (float)currentPlayerHp.RuntimeValue / maxPlayerHp.RuntimeValue;
            playerHpSprite.transform.localScale = new Vector3(hpPercent, 1, 1);                    
        }
    }
}
