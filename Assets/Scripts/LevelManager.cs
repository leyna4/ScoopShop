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
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI targetText;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI levelCompleteTitleText;

    private int currentCoins = 0;
    private bool levelDone = false;

    void Start()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        currentLevelIndex = index;
        currentCoins = 0;
        levelDone = false;

        LevelConfig config = levels[currentLevelIndex];

        if (levelText != null)
            levelText.text = "Day " + (currentLevelIndex + 1);
        if (targetText != null)
            targetText.text = "Target: " + config.coinTarget;
        if (coinText != null)
            coinText.text = "Coins: 0";

        BeadSpawner bs = FindObjectOfType<BeadSpawner>();
        if (bs != null)
        {
            bs.activeBoncukTypes = config.activeBoncukTypes;
            bs.minPerType = config.minBeadsPerType;
            bs.maxPerType = config.maxBeadsPerType;
        }

        // Sahneyi sýfýrla
        FindObjectOfType<GameManager>().StartScoopPhase();
        FindObjectOfType<PhoneController>().RingAgain(false);

        Debug.Log("Level yüklendi: " + config.levelName);
    }

    public void AddCoins(int amount)
    {
        if (levelDone) return;

        currentCoins += amount;
        if (coinText != null)
            coinText.text = "Coins: " + currentCoins;

        LevelConfig config = levels[currentLevelIndex];
        if (currentCoins >= config.coinTarget)
        {
            levelDone = true;
            Debug.Log("HEDEFE ULAÞILDI!");
        }
    }

    public bool IsLevelDone() => levelDone;

    public void LevelComplete()
    {
        Debug.Log("LevelComplete çaðrýldý - index: " + currentLevelIndex);

        PhoneController pc = FindObjectOfType<PhoneController>();
        if (pc != null) pc.enabled = false;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = false;

        LevelConfig config = levels[currentLevelIndex];
        Debug.Log("Daily Cost: " + config.dailyCost);

        if (config.dailyCost > 0)
        {
            ExpenseManager em = FindObjectOfType<ExpenseManager>();
            if (em != null)
            {
                Debug.Log("ExpenseManager bulundu, ShowExpenses çaðrýlýyor");
                em.ShowExpenses(currentCoins);
            }
            else
                Debug.LogError("ExpenseManager bulunamadý!");
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

    public void OnNextLevelClicked()
    {
        levelCompletePanel.SetActive(false);

        // Etkileþimleri aç
        PhoneController pc = FindObjectOfType<PhoneController>();
        if (pc != null) pc.enabled = true;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = true;

        bool isLastLevel = currentLevelIndex >= levels.Count - 1;

        if (isLastLevel)
            LoadLevel(currentLevelIndex); // son levelse ayný leveli tekrar baþlat (endless)
        else
            LoadLevel(currentLevelIndex + 1);
    }

    public void OnRestartClicked()
    {
        gameOverPanel.SetActive(false);
        LoadLevel(currentLevelIndex);
    }
}