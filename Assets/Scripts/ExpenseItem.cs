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

    void OnEnable()
    {
        Reset();
    }

    public void OnCheckboxClicked()
    {
        if (isPaid) return;

        isPaid = true;
        if (checkmark != null)
            checkmark.SetActive(true);

        FindObjectOfType<ExpenseManager>().OnExpensePaid();
    }

    public void Reset()
    {
        isPaid = false;
        if (checkmark != null)
            checkmark.SetActive(false);
    }
}