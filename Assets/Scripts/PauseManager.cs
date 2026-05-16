using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public KeyCode pauseKey = KeyCode.Escape;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
                Continue();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);

        // Saati durdur
        DayClockManager clock = FindObjectOfType<DayClockManager>();
        if (clock != null) clock.StopClock();
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        // Saati devam ettir
        DayClockManager clock = FindObjectOfType<DayClockManager>();
        if (clock != null) clock.ResumeClock();
    }

    public void TryAgain()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        // Kaldýðý leveldan baþlat
        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            lm.RestartCurrentLevel();
    }
}