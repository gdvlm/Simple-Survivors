using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject guiCanvas;

    void Start()
    {
        startMenuCanvas.SetActive(true);
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        guiCanvas.SetActive(true);
    }

    public void ReturnToStartMenu()
    {
        startMenuCanvas.SetActive(true);
        guiCanvas.SetActive(false);
    }
}
