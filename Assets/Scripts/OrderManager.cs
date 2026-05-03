using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coins = 0;

    public int dailyCost = 20; // Günlük gider

    public void Deliver(int score)
    {
        coins += score;
        coinText.text = "Coins: " + coins;
        Debug.Log("Kazanýlan: " + score + " | Toplam: " + coins);
        CheckEndDay();
    }

    void CheckEndDay()
    {
        if (coins >= dailyCost)
        {
            Debug.Log("Next Day");
        }
        else
        {
            Debug.Log("GAME OVER");
        }
    }
}