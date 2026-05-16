using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configs")]
    public List<LevelConfig> levels;
    private int currentLevelIndex = 0;

    [Header("UI")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI targetText;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI levelCompleteTitleText;

    private int levelCoins = 0;
    private bool levelDone = false;

    void Start()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        levelCoins = 0;
        levelDone = false;

        LevelConfig config = levels[currentLevelIndex];

        if (levelText != null)
            levelText.text = "Day " + (currentLevelIndex + 1);
        if (targetText != null)
            targetText.text = "Target: " + config.coinTarget;

        BeadSpawner bs = FindObjectOfType<BeadSpawner>();
        if (bs != null)
        {
            bs.activeBoncukTypes = config.activeBoncukTypes;
            bs.minPerType = config.minBeadsPerType;
            bs.maxPerType = config.maxBeadsPerType;
        }

        PackingManager pm = FindObjectOfType<PackingManager>(true);
        if (pm != null)
            pm.levelConfig = config;

       

        FindObjectOfType<GameManager>().StartScoopPhase();
        FindObjectOfType<PhoneController>().RingAgain(false);

        Debug.Log("Level yüklendi: " + config.levelName);
        FindObjectOfType<DayClockManager>().StartClock(currentLevelIndex + 1);
    }

    public void AddCoins(int amount)
    {
        if (levelDone) return;

        levelCoins += amount;

        LevelConfig config = levels[currentLevelIndex];
        if (levelCoins >= config.coinTarget)
        {
            levelDone = true;
            Debug.Log("HEDEFE ULAÞILDI!");
            // Saati durdur, level complete
            FindObjectOfType<DayClockManager>().EndDayEarly();
            LevelComplete();
        }
    }

    public bool IsLevelDone() => levelDone;

    public void LevelComplete()
    {
        PhoneController pc = FindObjectOfType<PhoneController>();
        if (pc != null) pc.enabled = false;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = false;

        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null) timer.StopTimer();

        LevelConfig config = levels[currentLevelIndex];

        if (config.dailyCost > 0)
        {
            ExpenseManager em = FindObjectOfType<ExpenseManager>();
            if (em != null)
                em.ShowExpenses();
        }
        else
        {
            if (levelCompletePanel != null)
            {
                levelCompletePanel.SetActive(true);
                if (levelCompleteTitleText != null)
                    levelCompleteTitleText.text = "Day " + (currentLevelIndex + 1) + " Complete!";
            }
        }
    }

    public void OnTimeUp()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void OnOrderTimeUp()
    {
        FindObjectOfType<PackingManager>().ForceDeliver();
    }

    public void OnNextLevelClicked()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        PhoneController pc = FindObjectOfType<PhoneController>();
        if (pc != null) pc.enabled = true;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = true;

        bool isLastLevel = currentLevelIndex >= levels.Count - 1;
        if (isLastLevel)
            LoadLevel(currentLevelIndex);
        else
            LoadLevel(currentLevelIndex + 1);
    }

    public void OnRestartClicked()
    {
        gameOverPanel.SetActive(false);
        LoadLevel(currentLevelIndex);
    }

    public LevelConfig GetCurrentConfig()
    {
        if (currentLevelIndex < levels.Count)
            return levels[currentLevelIndex];
        return null;
    }
    public void OnDayFailed()
    {
        Debug.Log("GAME OVER - Hedefe ulaþýlamadý!");
        PhoneController pc = FindObjectOfType<PhoneController>();
        if (pc != null) pc.enabled = false;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
    public void RestartCurrentLevel()
    {
        // Önce sahneyi sýfýrla
        FindObjectOfType<GameManager>().ResetScene();
        // Sonra leveli yükle
        LoadLevel(currentLevelIndex);
    }
}