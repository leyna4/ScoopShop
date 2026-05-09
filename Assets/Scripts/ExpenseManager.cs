using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ExpenseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject expensePanel;
    public Button doneButton;
    public TextMeshProUGUI coinText;

    [Header("Expenses")]
    public List<ExpenseItem> expenseItems;

    private int currentCoins = 0;
    private int paidCount = 0;

    void Start()
    {
        expensePanel.SetActive(false);
        if (doneButton != null)
            doneButton.gameObject.SetActive(false);
    }

    public void ShowExpenses(int coins)
    {
        Debug.Log("ShowExpenses çaðrýldý - coins: " + coins);
        Debug.Log("expensePanel: " + expensePanel);

        currentCoins = coins;
        paidCount = 0;

        foreach (ExpenseItem item in expenseItems)
            item.Reset();

        expensePanel.SetActive(true);
        Debug.Log("expensePanel active: " + expensePanel.activeSelf);

        doneButton.gameObject.SetActive(false);
        UpdateCoinText();
    }
    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UpdateCoinText();
            return true;
        }
        return false;
    }

    public void OnExpensePaid()
    {
        paidCount++;
        // Tüm giderler ödendi mi?
        if (paidCount >= expenseItems.Count)
            doneButton.gameObject.SetActive(true);
    }

    void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = "" + currentCoins;
    }

    public void OnDoneClicked()
    {
        expensePanel.SetActive(false);
        FindObjectOfType<LevelManager>().OnNextLevelClicked();
    }
}