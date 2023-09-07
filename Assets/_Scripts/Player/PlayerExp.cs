using TMPro;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private LevelExpMapping[] experienceTable;
        [SerializeField] private TMP_Text levelText;
        
        private int _experiencePoints = 0;
        private int _level = 1;

        void Start()
        {
            if (experienceTable.Length == 0)
            {
                Debug.LogError($"{nameof(experienceTable)} is missing mappings.", this);
            }

            if (levelText == null)
            {
                Debug.LogError($"{nameof(levelText)} is not assigned.", this);
            }
        }

        private void LevelUp()
        {
            _experiencePoints -= experienceTable[_level - 1].experiencePoints;
            _level++;

            levelText.text = $"LV {_level}";
        }
        
        public void ResetExp()
        {
            _experiencePoints = 0;
        }

        public void GainExp(int experiencePoints)
        {
            _experiencePoints += experiencePoints;

            while (_experiencePoints >= experienceTable[_level - 1].experiencePoints
                   && experienceTable[^1].level > _level)
            {
                LevelUp();
            }
        }
    }
}
