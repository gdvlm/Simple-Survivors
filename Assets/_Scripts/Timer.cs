using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private float _currentTime;
    private bool _isRunning;
    
    void Update()
    {
        if (_isRunning)
        {
            _currentTime += Time.deltaTime;
            timerText.text = GetTime();
        }
    }

    private string GetTime()
    {
        int seconds = (int)_currentTime % 60;
        int minutes = (int)_currentTime / 60;

        return $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        _currentTime = 0f;
        _isRunning = true;
    }

    public void ResetTimer()
    {
        _currentTime = 0;
    }

    public void PauseTimer()
    {
        _isRunning = false;
    }

    public void ResumeTimer()
    {
        _isRunning = true;
    }
}
