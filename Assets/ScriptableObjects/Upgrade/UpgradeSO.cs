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

        public string title;
        public string description;
    }
}
