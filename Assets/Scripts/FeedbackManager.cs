using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedbackManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI coinRewardText;
    public Button closeButton;

    private bool pendingIsCorrect;
    private int pendingCoins;

    void Start()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(OnClose);
    }

    public void SetPendingFeedback(bool isCorrect, int coins)
    {
        pendingIsCorrect = isCorrect;
        pendingCoins = coins;
    }

    public void ShowFeedback()
    {
        Debug.Log("ShowFeedback çaðrýldý");
        panel.SetActive(true);

        if (pendingIsCorrect)
        {
            feedbackText.text = "Great job! Perfect order!";
            coinRewardText.text = "+" + pendingCoins + " coins";
        }
        else
        {
            feedbackText.text = "Wrong order... Try harder!";
            coinRewardText.text = "+" + pendingCoins + " coins (half pay)";
        }
    }

    void OnClose()
    {
        panel.SetActive(false);

        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null && lm.IsLevelDone())
            lm.LevelComplete();
        else
        {
            FindObjectOfType<GameManager>().StartScoopPhase();
            FindObjectOfType<PhoneController>().RingAgain(false);
        }
    }
}