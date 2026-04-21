using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private int coins = 0;

    public int dailyCost = 20; // Günlük gider

    public void Deliver()
    {
        int pink = 5;
        int red = 3;
        int blue = 4;

        int total = pink * 2 + red * 3 + blue * 4;

        coins += total;

        coinText.text = "Coins: " + coins;

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