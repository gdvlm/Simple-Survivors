using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerExpUI : MonoBehaviour
    {
        [SerializeField] private Transform expBarSprite;
        [SerializeField] private FloatVariable playerExpPercent;
        
        void Update()
        {
            expBarSprite.localScale = new Vector3(playerExpPercent.RuntimeValue, 1, 1);
        }
    }
}
