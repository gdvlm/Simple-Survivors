using SimpleSurvivors.Player;
using SimpleSurvivors.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleSurvivors.Utils
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image image;

        private UpgradeSO _upgradeSo;
        private PlayerAttack _playerAttack;

        void Awake()
        {
            _playerAttack = player.GetComponent<PlayerAttack>();
        }

        public void UpdateUpgrade(UpgradeSO upgradeSo)
        {
            _upgradeSo = upgradeSo;
            title.text = upgradeSo.title;
            description.text = upgradeSo.description;
            // TODO: Map image here
        }

        public void ResumeGame()
        {
            _playerAttack.UpgradeAttack(_upgradeSo.attackMultiply);
            _playerAttack.UpgradeAttackDelay(_upgradeSo.attackSpeedAdd);
            // TODO: Add other upgrade effects here
            
            gameManager.ResumeGame();
        }
    }
}
