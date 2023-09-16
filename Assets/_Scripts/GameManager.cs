using System.Collections.Generic;
using SimpleSurvivors.Enemy;
using SimpleSurvivors.Player;
using SimpleSurvivors.Utils;
using TMPro;
using UnityEngine;

namespace SimpleSurvivors
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenuCanvas;
        [SerializeField] private GameObject guiCanvas;
        [SerializeField] private GameObject defeatCanvas;
        [SerializeField] private GameObject levelUpCanvas;
        [SerializeField] private Transform upgradeButtons;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private Timer timer;
        [SerializeField] private UpgradeManager upgradeManager;

        private PlayerInput _playerInput;
        private PlayerAttack _playerAttack;
        private List<Transform> _upgradeButtons = new();

        void Awake()
        {
            _playerInput = playerHealth.GetComponent<PlayerInput>();
            _playerAttack = playerHealth.GetComponent<PlayerAttack>();

            for (int i = 0; i < upgradeButtons.childCount; i++)
            {
                _upgradeButtons.Add(upgradeButtons.GetChild(i));
            }
        }

        void Start()
        {
            startMenuCanvas.SetActive(true);
            defeatCanvas.SetActive(false);
        }

        public void StartGame()
        {
            startMenuCanvas.SetActive(false);
            guiCanvas.SetActive(true);
            playerHealth.ReadyPlayer();
            enemySpawner.ResetEnemies();
            timer.StartTimer();
            _playerInput.SetCanMove(true);
        }

        public void ReturnToStartMenu()
        {
            defeatCanvas.SetActive(false);
            startMenuCanvas.SetActive(true);
            guiCanvas.SetActive(false);
            _playerInput.SetCanMove(false);
        }

        public void PauseGame()
        {
            timer.PauseTimer();
            enemySpawner.PauseEnemyMovements();
            _playerInput.SetCanMove(false);
            _playerAttack.SetAttack(false);
        }
        
        public void ResumeGame()
        {
            timer.ResumeTimer();
            enemySpawner.ResumeEnemyMovements();
            _playerInput.SetCanMove(true);
            _playerAttack.SetAttack(true);
            levelUpCanvas.SetActive(false);
        }

        public void DisplayLevelUpCanvas()
        {
            var upgradeSos = upgradeManager.GetRandomUpgrades(3);
            for (int i = 0; i < upgradeSos.Length; i++)
            {
                var upgradeItem = _upgradeButtons[i].GetComponent<UpgradeItem>();
                upgradeItem.UpdateUpgrade(upgradeSos[i]);
            }
            
            levelUpCanvas.SetActive(true);
        }

        //public void SelectUpgrade(UpgradeSO upgradeSo)
        public void SelectUpgrade()
        {
            // TODO: Somehow pass upgradeSo here from button to apply to character
            levelUpCanvas.SetActive(false);
            ResumeGame();
        }
    }
}