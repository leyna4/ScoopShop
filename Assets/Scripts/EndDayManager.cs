using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDayManager : MonoBehaviour
{
    public Button endDayButton;
    public Button nextDayButton;
    public TextMeshProUGUI taskText;

    void Start()
    {
        if (endDayButton != null)
            endDayButton.interactable = false;
        if (nextDayButton != null)
            nextDayButton.gameObject.SetActive(false);
        if (taskText != null)
            taskText.text = "Earn Coins";
    }

    public void EnableEndDay()
    {
        gameObject.SetActive(true);
        Debug.Log("EnableEndDay çalıştı");
        if (endDayButton != null)
        {
            endDayButton.interactable = true;
            Debug.Log("Button aktif edildi");
        }
        else
        {
            Debug.LogError("endDayButton NULL! Inspector'da bağla.");
        }
    }

    public void OnEndDayClicked()
    {
        if (taskText != null)
            taskText.text = "Day Complete!";
        if (nextDayButton != null)
            nextDayButton.gameObject.SetActive(true);
        FindObjectOfType<TutorialManager>().tutorialText.text = "Day Complete!";
    }

    public void OnNextDay()
    {
        Debug.Log("NEXT DAY");
    }
}