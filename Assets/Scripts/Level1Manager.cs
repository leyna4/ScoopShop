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
            Debug.Log("HEDEFE ULAÞILDI!");
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
        // Tüm etkileþimleri kilitle
        FindObjectOfType<PhoneController>().enabled = false;
        ScoopController sc = FindObjectOfType<ScoopController>();
        if (sc != null) sc.enabled = false;

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);
    }

    public bool IsLevelDone() => levelDone;

    public void OnNextLevelClicked()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Level1Scene");
    }
}