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
        private PlayerInput _playerInput;

        void Awake()
        {
            _playerAttack = player.GetComponent<PlayerAttack>();
            _playerInput = player.GetComponent<PlayerInput>();
        }

        public void UpdateUpgrade(UpgradeSO upgradeSo)
        {
            _upgradeSo = upgradeSo;
            title.text = upgradeSo.title;
            description.text = upgradeSo.description;
            image.sprite = upgradeSo.sprite;
        }

        public void SelectUpgrade()
        {
            _playerAttack.UpgradeAttack(_upgradeSo.attackMultiply);
            _playerAttack.UpgradeAttackDelay(_upgradeSo.attackSpeedAdd);
            _playerInput.UpgradeMovementSpeed(_upgradeSo.movementSpeedMultiply);
            
            gameManager.ResumeGame();
        }
    }
}
