using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject guiCanvas;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject enemyContainer;

    private EnemyMovement[] _enemyMovements;

    void Awake()
    {
        _enemyMovements = enemyContainer.GetComponentsInChildren<EnemyMovement>();
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
        
        foreach (EnemyMovement enemyMovement in _enemyMovements)
        {
            enemyMovement.ResetRandomPosition();
        }
    }

    public void ReturnToStartMenu()
    {
        defeatCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
        guiCanvas.SetActive(false);
    }
}
