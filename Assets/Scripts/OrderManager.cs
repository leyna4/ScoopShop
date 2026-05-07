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
        coinText.text = "Coins: " + coins;

        // Level1Manager'a coin bildir
        Level1Manager lm = FindObjectOfType<Level1Manager>();
        if (lm != null)
            lm.AddCoins(score);

        FindObjectOfType<PhoneController>().RingAgain(true);
    }
}