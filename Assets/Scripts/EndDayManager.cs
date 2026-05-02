using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDayManager : MonoBehaviour
{
    public Button endDayButton;
    public Button nextDayButton;

    public Image checkbox;
    public TextMeshProUGUI taskText;

    private bool taskCompleted = false;

    void Start()
    {
        endDayButton.interactable = false;
        nextDayButton.gameObject.SetActive(false);

        taskText.text = "⬜ Earn Coins";
    }

    // 💰 Coin kazanıldıktan sonra çağrılacak
    public void EnableEndDay()
    {
        taskCompleted = true;
        endDayButton.interactable = true;
    }

    // 📋 End Day butonuna basınca
    public void OnEndDayClicked()
    {
        if (!taskCompleted) return;

        // checkbox tik
        checkbox.color = Color.green;
        taskText.text = "☑ Earn Coins";

        // tutorial ilerlet
        FindObjectOfType<TutorialManager>().tutorialText.text = "Day Complete!";

        // next day aç
        nextDayButton.gameObject.SetActive(true);
    }

    public void OnNextDay()
    {
        Debug.Log("NEXT DAY");
    }
}