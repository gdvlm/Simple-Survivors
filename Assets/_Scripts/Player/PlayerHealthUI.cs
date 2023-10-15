using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Transform playerHpSprite;
        [SerializeField] private IntVariable maxPlayerHp;
        [SerializeField] private IntVariable currentPlayerHp;

        void Update()
        {
            float hpPercent = (float)currentPlayerHp.RuntimeValue / maxPlayerHp.RuntimeValue;
            playerHpSprite.localScale = new Vector3(hpPercent, 1, 1);                    
        }
    }
}
