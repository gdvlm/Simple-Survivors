using UnityEngine;

namespace SimpleSurvivors.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Template", menuName = "ScriptableObjects/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public int enemyHp;
    }
}
