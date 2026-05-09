using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpenseItem : MonoBehaviour
{
    public TextMeshProUGUI expenseText;
    public GameObject checkmark;
    public int cost;
    public string expenseName;

    private bool isPaid = false;

    void Start()
    {
        checkmark.SetActive(false);
    }

    public void OnCheckboxClicked()
    {
        if (isPaid) return;

        ExpenseManager em = FindObjectOfType<ExpenseManager>();
        if (em == null) return;

        if (em.SpendCoins(cost))
        {
            isPaid = true;
            checkmark.SetActive(true);
            em.OnExpensePaid();
            Debug.Log(expenseName + " ödendi: -" + cost);
        }
        else
        {
            Debug.Log("Yeterli coin yok!");
        }
    }

    public void Reset()
    {
        isPaid = false;
        checkmark.SetActive(false);
    }
}