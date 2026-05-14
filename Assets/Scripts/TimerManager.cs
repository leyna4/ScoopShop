using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public GameObject timerPanel;

    private float timeLeft;
    private bool isRunning = false;
    private float orderSeconds = 60f;
    void Start()
    {
        isRunning = false;
        if (timerPanel != null)
            timerPanel.SetActive(false);
    }
    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            isRunning = false;
            OnTimeUp();
        }

        UpdateTimerUI();
    }

    public void StartOrderTimer(float seconds)
    {
        orderSeconds = seconds;
        timeLeft = seconds;
        isRunning = true;
        if (timerPanel != null)
            timerPanel.SetActive(true);
    }

    public void StopTimer()
    {
        isRunning = false;
        if (timerPanel != null)
            timerPanel.SetActive(false);
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        if (timerText != null)
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (timerText != null)
            timerText.color = timeLeft <= 10 ? Color.red : Color.white;
    }

    void OnTimeUp()
    {
        Debug.Log("SÜRE BÝTTÝ!");
        // Sipariþ iptal — yarý puan ver
        FindObjectOfType<LevelManager>().OnOrderTimeUp();
    }
}