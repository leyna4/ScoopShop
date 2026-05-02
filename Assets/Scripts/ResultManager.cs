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
        pink = p;
        red = r;
        blue = b;

        panel.SetActive(true);

        customerText.text = "Customer 1";

        itemListText.text =
            p + " Hair Clips\n" +
            r + " Wet Wipes\n" +
            b + " Nail Polish";
    }
}