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
        coinText.text = "" + coins;

        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            lm.AddCoins(score);

        FeedbackManager fm = FindObjectOfType<FeedbackManager>(true);
        if (fm != null)
            fm.SetPendingFeedback(isCorrect, score);

        FindObjectOfType<PhoneController>().RingAgain(true);
    }

    public int GetCoins()
    {
        return coins;
    }
    public void DeductCoins(int amount)
    {
        coins -= amount;
        if (coins < 0) coins = 0;
        coinText.text = "" + coins;
        Debug.Log("Gider düþüldü: -" + amount + " | Kalan: " + coins);
    }
}