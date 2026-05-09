using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    
    public TextMeshProUGUI tutorialText;
    public GameObject endDayPanel;

    public enum Step
    {
        Phone, ReadMessage, PickSpoon, MoveToJar,
        ScoopDone, MoveToPlate, Pour, CheckResult,
        PackItems, ReadyToSend, Deliver, FeedbackCall,
        NewOrder, Done
    }

    public Step currentStep;

    void Start()
    {
        currentStep = Step.Phone;
        tutorialText.text = "Your phone is ringing! Tap it.";
    }

    public void OnPhoneOpened()
    {
        currentStep = Step.ReadMessage;
        tutorialText.text = "Read your customer's order!";
    }

    public void OnScoopDone()
    {
        currentStep = Step.ScoopDone;
        tutorialText.text = "Great! Now drag the spoon to the plate.";
    }

    public void OnPour()
    {
        currentStep = Step.CheckResult;
        tutorialText.text = "Check the result and press the arrow!";
    }

    public void OnPackingStarted()
    {
        currentStep = Step.PackItems;
        tutorialText.text = "Drag items from the shelves into the box!";
    }

    public void OnItemPacked()
    {
        if (currentStep == Step.PackItems)
        {
            currentStep = Step.ReadyToSend;
            tutorialText.text = "Keep packing! Press Ready when done.";
        }
    }

    public void OnReadyPressed()
    {
        currentStep = Step.Deliver;
        tutorialText.text = "Press Deliver to send the order!";
    }

    public void OnDeliverPressed(bool isCorrect)
    {
        currentStep = Step.FeedbackCall;
        if (isCorrect)
            tutorialText.text = "Perfect order! Wait for customer feedback.";
        else
            tutorialText.text = "Wrong order... Wait for customer feedback.";
    }

    public void OnFeedbackCall()
    {
        tutorialText.text = "Customer is calling! Tap the phone.";
    }

    public void OnNewOrder()
    {
        currentStep = Step.Phone;
        tutorialText.text = "New order incoming! Tap your phone.";
    }

    public void OnCoinsDone()
    {
        tutorialText.text = "Press End Day when ready!";
    }

    public void OnConfirm()
    {
        currentStep = Step.Done;
        tutorialText.text = "Well done! You completed the tutorial!";
    }

    public void OnOrderSent()
    {
        tutorialText.text = "Order sent! Well done!";
    }
}