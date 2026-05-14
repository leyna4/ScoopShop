using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ExpenseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject expensePanel;
    public Button doneButton;

    [Header("Expenses")]
    public List<ExpenseItem> expenseItems;
    public int totalExpenseCost = 33;

    private int paidCount = 0;

    void Start()
    {
        expensePanel.SetActive(false);
        if (doneButton != null)
            doneButton.gameObject.SetActive(false);
    }

    public void ShowExpenses()
    {
        paidCount = 0;

        foreach (ExpenseItem item in expenseItems)
            item.Reset();

        LevelConfig config = FindObjectOfType<LevelManager>().GetCurrentConfig();
        Debug.Log("Config: " + config.levelName + " | Expense count: " + config.expenseNames.Count);

        if (config != null && config.expenseNames.Count > 0)
        {
            List<int> indices = GetRandomIndices(config.expenseNames.Count, expenseItems.Count);
            totalExpenseCost = 0;

            for (int i = 0; i < expenseItems.Count; i++)
            {
                int idx = indices[i];
                expenseItems[i].expenseName = config.expenseNames[idx];
                expenseItems[i].cost = config.expenseCosts[idx];
                expenseItems[i].expenseText.text = config.expenseNames[idx] +
                    " (-" + config.expenseCosts[idx] + " coins)";
                totalExpenseCost += config.expenseCosts[idx];
                Debug.Log("Gider " + i + ": " + config.expenseNames[idx] + " - " + config.expenseCosts[idx]);
            }
        }

        expensePanel.SetActive(true);
        doneButton.gameObject.SetActive(false);
    }

    List<int> GetRandomIndices(int max, int count)
    {
        List<int> all = new List<int>();
        for (int i = 0; i < max; i++) all.Add(i);

        List<int> result = new List<int>();
        for (int i = 0; i < count && all.Count > 0; i++)
        {
            int r = Random.Range(0, all.Count);
            result.Add(all[r]);
            all.RemoveAt(r);
        }
        return result;
    }
    public void OnExpensePaid()
    {
        paidCount++;
        if (paidCount >= expenseItems.Count)
            doneButton.gameObject.SetActive(true);
    }

    public void OnDoneClicked()
    {
        OrderManager om = FindObjectOfType<OrderManager>();
        if (om != null)
            om.DeductCoins(totalExpenseCost);

        expensePanel.SetActive(false);

        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm != null)
            lm.OnNextLevelClicked();
    }
}