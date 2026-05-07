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

        Level1Manager lm = FindObjectOfType<Level1Manager>();
        if (lm != null && lm.IsLevelDone())
        {
            Debug.Log("Hedef tamamlandý, End Day bekle");
        }
        else
        {
            FindObjectOfType<GameManager>().StartScoopPhase();
            FindObjectOfType<PhoneController>().RingAgain(false);
        }
    }
}