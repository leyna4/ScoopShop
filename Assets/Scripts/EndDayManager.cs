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

        Level1Manager lm = FindObjectOfType<Level1Manager>();
        if (lm != null)
            lm.LevelComplete();
        else
            Debug.LogError("Level1Manager bulunamadı!");
    }
    public void OnNextDay()
    {
        Debug.Log("NEXT DAY");
    }
}