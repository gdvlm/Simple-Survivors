using SimpleSurvivors.Player;
using SimpleSurvivors.Upgrade;
using SimpleSurvivors.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSurvivors.Utils
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField] private PlayerExp playerExp;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image image;
        [SerializeField] private IntVariable maxPlayerHp;
        [SerializeField] private FloatVariable movementSpeed;
        [SerializeField] private IntVariable attackDamage;
        [SerializeField] private FloatVariable attackDelay;

        private UpgradeSO _upgradeSo;

        public void UpdateUpgrade(UpgradeSO upgradeSo)
        {
            _upgradeSo = upgradeSo;
            title.text = upgradeSo.title;
            description.text = upgradeSo.description;
            image.sprite = upgradeSo.sprite;
        }

        public void SelectUpgrade()
        {
            _upgradeSo.ApplyUpgrade(maxPlayerHp, movementSpeed, attackDamage, attackDelay);
            gameManager.ResumeGame();
            
            // Handle leveling multiple times consecutively - this could be an event
            playerExp.GainExp(0);
        }
    }
}
