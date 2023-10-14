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
        private readonly float _minimumDelay = 0.1f;

        public void ApplyUpgrade(IntVariable maxPlayerHp, FloatVariable movementSpeed,
            IntVariable attackDamage, FloatVariable attackDelay)
        {
            UpgradeHealth(maxPlayerHp);
            UpgradeMovementSpeed(movementSpeed);
            UpgradeDamage(attackDamage);
            UpgradeAttackDelay(attackDelay);
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

        private void UpgradeDamage(IntVariable attackDamage)
        {
            attackDamage.RuntimeValue = (int)(attackDamage.RuntimeValue * attackMultiply);
        }

        private void UpgradeAttackDelay(FloatVariable attackDelay)
        {
            if (attackDelay.RuntimeValue <= _minimumDelay)
            {
                return;
            }

            attackDelay.RuntimeValue -= attackSpeedAdd;            
        }
    }
}