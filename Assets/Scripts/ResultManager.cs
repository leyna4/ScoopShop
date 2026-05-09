using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI customerText;
    public TextMeshProUGUI itemListText;
    public int pink;
    public int red;
    public int blue;

    public void ShowResult(int p, int r, int b)
    {
        Debug.Log("ShowResult çaðrýldý: " + p + " " + r + " " + b);
        pink = p; red = r; blue = b;

        GameObject orderPanel = GameObject.Find("MessagePanel");
        if (orderPanel != null) orderPanel.SetActive(false);

        panel.SetActive(true);
        customerText.text = "Customer 1";
        itemListText.text =
            p + " Hair Clips\n" +
            r + " Wet Wipes\n" +
            b + " Nail Polish";
    }

    public void ShowPanel()
    {
        if (panel != null) panel.SetActive(true);
    }

    public void HidePanel()
    {
        if (panel != null) panel.SetActive(false);
    }

    public void OnNextArrowClicked()
    {
        panel.SetActive(false);
        FindObjectOfType<GameManager>().StartPackingPhase();
    }
}