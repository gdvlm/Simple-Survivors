using System.Collections.Generic;
using SimpleSurvivors.Enemy;
using SimpleSurvivors.Player;
using SimpleSurvivors.Utils;
using UnityEngine;

namespace SimpleSurvivors
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenuCanvas;
        [SerializeField] private GameObject guiCanvas;
        [SerializeField] private GameObject defeatCanvas;
        [SerializeField] private GameObject levelUpCanvas;
        [SerializeField] private GameObject pauseMenuCanvas;
        [SerializeField] private Transform upgradeButtons;
        [SerializeField] private Player.Player player;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private HealthSpawner healthSpawner;
        [SerializeField] private Timer timer;
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private MusicManager musicPlayer;
        [SerializeField] private SoundEffectManager soundEffectManager;
        [SerializeField] private GameObject levelUpParticles;

        private PlayerInput _playerInput;
        private PlayerAttack _playerAttack;
        private List<Transform> _upgradeButtons = new();

        void Awake()
        {
            _playerInput = player.GetComponent<PlayerInput>();
            _playerAttack = player.GetComponent<PlayerAttack>();

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
            player.Initialize();
            enemySpawner.ResetEnemies();
            timer.StartTimer();
            _playerInput.SetCanMove(true);
            healthSpawner.RemoveAllHealth();
            healthSpawner.SetEnabled(true);
            musicPlayer.Play();
        }

        public void ReturnToStartMenu()
        {
            defeatCanvas.SetActive(false);
            startMenuCanvas.SetActive(true);
            guiCanvas.SetActive(false);
            _playerInput.SetCanMove(false);
            musicPlayer.Stop();
            soundEffectManager.StopSoundEffects();
        }

        public void PauseGame()
        {
            timer.PauseTimer();
            enemySpawner.PauseEnemyMovements();
            _playerInput.SetCanMove(false);
            _playerAttack.SetAttack(false);
            musicPlayer.Pause();
        }

        public void ResumeGame()
        {
            timer.ResumeTimer();
            enemySpawner.ResumeEnemyMovements();
            _playerInput.SetCanMove(true);
            _playerAttack.SetAttack(true);
            levelUpCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(false);
            levelUpParticles.SetActive(false);
            musicPlayer.Play();
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
            levelUpParticles.SetActive(true);
        }
    }
}