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
        FindObjectOfType<PhoneController>().RingAgain(true); // feedback modu
    }
}