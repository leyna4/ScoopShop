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

    public void EnableEndDay()
    {
        taskCompleted = true;
        endDayButton.interactable = true;
    }

   
    public void OnEndDayClicked()
    {
        if (!taskCompleted) return;

        
        checkbox.color = Color.green;
        taskText.text = "☑ Earn Coins";

       
        FindObjectOfType<TutorialManager>().tutorialText.text = "Day Complete!";

       
        nextDayButton.gameObject.SetActive(true);
    }

    public void OnNextDay()
    {
        Debug.Log("NEXT DAY");
    }
}