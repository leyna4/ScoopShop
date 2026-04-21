using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
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
        currentStep = Step.Coins;
        tutorialText.text = "You earned coins!";
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
}