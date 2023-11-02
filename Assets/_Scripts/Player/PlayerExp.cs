using SimpleSurvivors.Utils;
using SimpleSurvivors.Variables;
using UnityEngine;

namespace SimpleSurvivors.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private SoundEffectManager soundEffectManager;
        [SerializeField] private IntVariable playerLevel;
        [SerializeField] private FloatVariable playerExpPercent;
        
        private IExpRequirement _expRequirement;
        private int _totalExpPoints = 0;
        private int _currentExpPoints = 0;
        private int _totalExpNeeded = 0;

        void Start()
        {
            // This can be dynamically changed if classes were implemented
            _expRequirement = new DefaultExpRequirement();
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(playerLevel.RuntimeValue);
        }

        private void LevelUp()
        {
            playerExpPercent.RuntimeValue = GetExpPercent();
            playerLevel.RuntimeValue++;
            
            gameManager.PauseGame(false);
            gameManager.DisplayLevelUpCanvas();
            soundEffectManager.PlaySoundEffect(SoundEffect.LevelUp);
            
            _currentExpPoints = 0;
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(playerLevel.RuntimeValue);
            playerExpPercent.RuntimeValue = 0f;
        }

        private float GetExpPercent()
        {
            int expNeededForPrevLevel = playerLevel.RuntimeValue <= 1
                ? 0
                : _expRequirement.GetTotalExpRequirement(playerLevel.RuntimeValue - 1);

            _currentExpPoints = _totalExpPoints - expNeededForPrevLevel;
            int expNeededForCurrentLevel = _totalExpNeeded - expNeededForPrevLevel;
            
            return Mathf.Clamp((float)_currentExpPoints / expNeededForCurrentLevel, 0f, 100f);            
        }
        
        public void ResetLevel()
        {
            _totalExpPoints = 0;
            _currentExpPoints = 0;
            playerLevel.RuntimeValue = 1;
            playerExpPercent.RuntimeValue = 0f;
        }

        public void GainExp(int expPoints)
        {
            _totalExpPoints += _expRequirement.GetTotalGainedExp(playerLevel.RuntimeValue, expPoints);
            _totalExpNeeded = _expRequirement.GetTotalExpRequirement(playerLevel.RuntimeValue);
            playerExpPercent.RuntimeValue = GetExpPercent();

            if (_totalExpPoints >= _totalExpNeeded)
            {
                LevelUp();
            }
        }
    }
}
