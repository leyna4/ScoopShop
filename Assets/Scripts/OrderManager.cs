using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int dailyCost = 20;
    private int coins = 0;

    public void Deliver(int score, bool isCorrect)
    {
        coins += score;
        if (coinText != null)
            coinText.text = "" + coins;

        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            lm.AddCoins(score);

        FeedbackManager fm = FindObjectOfType<FeedbackManager>(true);
        if (fm != null)
            fm.SetPendingFeedback(isCorrect, score);

        FindObjectOfType<PhoneController>().RingAgain(true);
    }
}