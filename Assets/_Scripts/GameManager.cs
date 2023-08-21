using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject guiCanvas;
    [SerializeField] private GameObject playCanvas;

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        guiCanvas.SetActive(true);
        playCanvas.SetActive(true);
    }

    public void ReturnToStartMenu()
    {
        startMenuCanvas.SetActive(true);
        guiCanvas.SetActive(false);
        playCanvas.SetActive(false);
    }
}
