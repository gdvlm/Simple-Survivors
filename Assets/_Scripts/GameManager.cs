using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject guiCanvas;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Timer timer;

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
        enemySpawner.ResetEnemyPositions();
        timer.StartTimer();
    }

    public void ReturnToStartMenu()
    {
        defeatCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
        guiCanvas.SetActive(false);
    }
}
