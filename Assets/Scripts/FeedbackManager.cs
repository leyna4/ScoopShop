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
        panel.SetActive(false);
        StartCoroutine(ShowCompleteDelayed());
    }

    IEnumerator ShowCompleteDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<TutorialCompleteManager>().ShowTutorialComplete();
    }
}