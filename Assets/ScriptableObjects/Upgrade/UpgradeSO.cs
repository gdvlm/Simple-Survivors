using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Upgrade
{
    [CreateAssetMenu(fileName = "Upgrade Template", menuName = "ScriptableObjects/Upgrade")]
    public class UpgradeSO : ScriptableObject
    {
        public float attackAdd = 0;
        public float attackMultiply = 1;
        public float attackSpeedAdd = 0;
        public float attackSpeedMultiply = 1;
        public float movementSpeedMultiply = 1;
        public float healthMultiply = 1;
        public Sprite sprite;

        public string title;
        public string description;
        
        private readonly float _maximumMovementSpeed = 6f;

        public void ApplyUpgrade(IntVariable maxPlayerHp, FloatVariable movementSpeed)
        {
            UpgradeHealth(maxPlayerHp);
            UpgradeMovementSpeed(movementSpeed);
        }

        private void UpgradeHealth(IntVariable maxPlayerHp)
        {
            maxPlayerHp.RuntimeValue = (int)(maxPlayerHp.RuntimeValue * healthMultiply);
        }
        
        private void UpgradeMovementSpeed(FloatVariable movementSpeed)
        {
            if (movementSpeed.RuntimeValue >= _maximumMovementSpeed)
            {
                return;
            }
            
            movementSpeed.RuntimeValue *= movementSpeedMultiply;
        }        
    }
}
