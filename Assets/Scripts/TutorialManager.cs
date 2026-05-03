using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public EndDayManager endDayManager;
    public TextMeshProUGUI tutorialText;
    public GameObject endDayPanel;

    public enum Step
    {
        Phone,
        ReadMessage,
        PickSpoon,
        MoveToJar,
        ScoopDone,
        MoveToPlate,
        Pour,
        Coins,
        EndDay,
        Confirm,
        Done
    }

    public Step currentStep;

    void Start()
    {
        currentStep = Step.Phone;
        tutorialText.text = "Check your phone";
    }

    public void OnPhoneOpened()
    {
        currentStep = Step.PickSpoon;
        tutorialText.text = "Click the spoon";
    }

    public void OnScoopDone()
    {
        currentStep = Step.ScoopDone;
        tutorialText.text = "Scoop ready! Pick it again";
    }

    public void OnPour()
    {
        currentStep = Step.MoveToPlate;
        tutorialText.text = "Check your result and press ->";
    }

    public void OnPackingStarted()
    {
        Debug.Log("OnPackingStarted çaðrýldý");
        if (tutorialText == null)
            Debug.LogError("tutorialText NULL!");
        else
            tutorialText.text = "Drag items from the shelves into the box!";
    }

    public void OnCoinsDone()
    {
        tutorialText.text = "Press End Day";
    }

    public void OnConfirm()
    {
        currentStep = Step.Done;
        tutorialText.text = "Next Day!";
    }
    public void OnItemPacked()
    {
        tutorialText.text = "Keep packing! Press Ready when done.";
    }

    public void OnOrderSent()
    {
        tutorialText.text = "Order sent! Well done!";
    }
}