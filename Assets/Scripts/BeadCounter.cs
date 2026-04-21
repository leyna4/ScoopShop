using UnityEngine;
using TMPro;

public class BeadCounter : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    public void Count(int pink, int red, int blue)
    {
        resultText.text =
            pink + " Hair Clips\n" +
            red + " Wet Wipes\n" +
            blue + " Nail Polish";
    }
}