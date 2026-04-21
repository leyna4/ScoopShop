using UnityEngine;

public class ScoopController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isFilled = false;

    public Transform jarPoint;
    public Transform platePoint;

    public TutorialManager tutorial;

    void OnMouseDown()
    {
        if (tutorial.currentStep == TutorialManager.Step.PickSpoon)
        {
            isDragging = true;
            tutorial.currentStep = TutorialManager.Step.MoveToJar;
            tutorial.tutorialText.text = "Drag to jar";
        }

        else if (tutorial.currentStep == TutorialManager.Step.ScoopDone && isFilled)
        {
            isDragging = true;
            tutorial.currentStep = TutorialManager.Step.MoveToPlate;
            tutorial.tutorialText.text = "Drag to plate";
        }
    }

    void OnMouseUp()
    {
        if (tutorial.currentStep == TutorialManager.Step.MoveToJar)
        {
            isDragging = false;

            transform.position = jarPoint.position;
            isFilled = true;

            tutorial.OnScoopDone();
        }

        else if (tutorial.currentStep == TutorialManager.Step.MoveToPlate && isFilled)
        {
            isDragging = false;

            transform.position = platePoint.position;

            FindObjectOfType<BeadSpawner>().SpawnFixedBeads();

            tutorial.OnPour();
            FindObjectOfType<EndDayManager>().EnableEndDay();

            Invoke("CoinsDone", 1f);
            void CoinsDone()
            {
                FindObjectOfType<TutorialManager>().OnCoinsDone();
            }
        }
    }

    void CoinsDone()
    {
        FindObjectOfType<TutorialManager>().OnCoinsDone();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
}