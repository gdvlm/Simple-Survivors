using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject guiCanvas;
    [SerializeField] private GameObject defeatCanvas;

    void Start()
    {
        startMenuCanvas.SetActive(true);
        defeatCanvas.SetActive(false);
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        guiCanvas.SetActive(true);
    }

    public void ReturnToStartMenu()
    {
        defeatCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
        guiCanvas.SetActive(false);
    }
}
