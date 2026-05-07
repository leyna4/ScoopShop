using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FeedbackManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI coinRewardText;
    public Button closeButton;

    void Start()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(OnClose);
    }

    public void ShowFeedback(bool isCorrect, int coins)
    {
        panel.SetActive(true);

        if (isCorrect)
        {
            feedbackText.text = "Great job! Perfect order!";
            coinRewardText.text = "+" + coins + " coins";
        }
        else
        {
            feedbackText.text = "Wrong order... Try harder!";
            coinRewardText.text = "+" + coins + " coins (half pay)";
        }
    }

    void OnClose()
    {
        Debug.Log("OnClose çaðrýldý");
        panel.SetActive(false);

        TutorialCompleteManager tcm = FindObjectOfType<TutorialCompleteManager>(true);
        if (tcm != null)
        {
            Debug.Log("TutorialCompleteManager bulundu");
            tcm.ShowCompleteWithDelay();
        }
        else
            Debug.LogError("TutorialCompleteManager bulunamadý!");
    }
}