using UnityEngine;
using TMPro;

public class DayClockManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dayText;

    [Header("Ayarlar")]
    public float realSecondsPerDay = 180f; // 3 dakika = 1 oyun günü

    private float[] timeStops = { 9f, 12f, 15f, 18f };
    private float currentTime = 9f;
    private float elapsed = 0f;
    private bool isRunning = false;
    private int currentDay = 1;

    void Start()
    {
        StopClock();
    }

    void Update()
    {
        if (!isRunning) return;

        elapsed += Time.deltaTime;
        float t = elapsed / realSecondsPerDay;
        currentTime = Mathf.Lerp(9f, 18f, t);

        UpdateClockUI();

        if (currentTime >= 18f)
        {
            currentTime = 18f;
            UpdateClockUI();
            isRunning = false;
            OnDayEnd();
        }
    }

    public void StartClock(int day)
    {
        currentDay = day;
        currentTime = 9f;
        elapsed = 0f;
        isRunning = true;

        if (dayText != null)
            dayText.text = "Day " + currentDay;

        UpdateClockUI();
    }
    public void ResumeClock()
    {
        isRunning = true;
    }

    public void StopClock()
    {
        isRunning = false;
        UpdateClockUI();
    }


    void UpdateClockUI()
    {
        int hour = Mathf.FloorToInt(currentTime);
        int minute = Mathf.FloorToInt((currentTime - hour) * 60);

        // Sadece saat dilimlerini göster
        int displayHour = GetDisplayHour(currentTime);

        if (clockText != null)
            clockText.text = string.Format("{0:00}:00", displayHour);
    }

    int GetDisplayHour(float time)
    {
        if (time < 12f) return 9;
        else if (time < 15f) return 12;
        else if (time < 18f) return 15;
        else return 18;
    }

    void OnDayEnd()
    {
        Debug.Log("GÜN BÝTTÝ!");
        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
        {
            if (lm.IsLevelDone())
                lm.LevelComplete();
            else
                lm.OnDayFailed(); // hedefe ulaþýlmadý
        }
    }

    public void EndDayEarly()
    {
        // Hedefe ulaþýlýnca erken bitir
        isRunning = false;
        Debug.Log("Gün erken bitti - hedef tamamlandý!");
    }
    
}