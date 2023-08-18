using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject playCanvas;

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    public void ReturnToStartMenu()
    {
        playCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
    }
}
