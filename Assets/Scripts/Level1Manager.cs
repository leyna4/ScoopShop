using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    [Header("Level Config")]
    public LevelConfig levelConfig;

    [Header("UI")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI targetText;
    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    private int currentCoins = 0;
    private bool levelDone = false;

    void Start()
    {
        currentCoins = 0;
        levelDone = false;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        if (levelDone) return;

        currentCoins += amount;
        UpdateUI();

        if (currentCoins >= levelConfig.coinTarget)
        {
            levelDone = true;
            // Hedefe ulaþtý, End Day göster
            EndDayManager edm = FindObjectOfType<EndDayManager>();
            if (edm != null)
                edm.EnableEndDay();
            else
                Debug.LogError("EndDayManager bulunamadý!");
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + currentCoins;
        if (targetText != null)
            targetText.text = "Target: " + levelConfig.coinTarget;
    }

    public void LevelComplete()
    {
        Debug.Log("LEVEL TAMAMLANDI!");
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    public void OnNextLevelClicked()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    public bool IsLevelDone()
    {
        return levelDone;
    }
}