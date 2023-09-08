using TMPro;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        
        private IExpRequirement _expRequirement;
        private int _totalExpPoints = 0;
        private int _currentExpPoints = 0;
        private int _totalExpNeeded = 0;
        private int _level = 1;

        void Start()
        {
            if (levelText == null)
            {
                Debug.LogError($"{nameof(levelText)} is not assigned.", this);
            }
            
            // This can be dynamically changed if classes were implemented
            _expRequirement = new DefaultExpRequirement();
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(_level);
        }

        private void LevelUp()
        {
            float expPercent = GetExpPercent();         
            UpdateExpBar(expPercent);
            _level++;
            
            // TODO: Show leveled UI here (animation, sounds, etc)
            
            
            _currentExpPoints = 0;
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(_level);
            UpdateExpBar(0f);

            levelText.text = $"LV {_level}";
        }

        private float GetExpPercent()
        {
            int expNeededForPrevLevel = _level <= 1
                ? 0
                : _expRequirement.GetTotalExpRequirement(_level - 1);

            _currentExpPoints = _totalExpPoints - expNeededForPrevLevel;
            int expNeededForCurrentLevel = _totalExpNeeded - expNeededForPrevLevel;
            
            return Mathf.Clamp((float)_currentExpPoints / expNeededForCurrentLevel, 0f, 100f);            
        }

        private void UpdateExpBar(float expPercent)
        {
            print($"ExpPercent: {expPercent:P}");
            // TODO: Update exp bar sprite
            //playerExpSprite.transform.localScale = new Vector3(expPercent, 1, 1);
        }
        
        public void ResetExp()
        {
            _totalExpPoints = 0;
            _currentExpPoints = 0;
            UpdateExpBar(0f);
        }

        public void GainExp(int expPoints)
        {
            _totalExpPoints += _expRequirement.GetTotalGainedExp(_level, expPoints);
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(_level);

            float expPercent = GetExpPercent();
            UpdateExpBar(expPercent);
            
            while (_totalExpPoints >= _totalExpNeeded)
            {
                LevelUp();
            }
        }
    }
}
