using UnityEngine;

public class ScoopController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isFilled = false;

    public TutorialManager tutorial;

    private string currentZone = "";

    void OnMouseDown()
    {
        if (tutorial.currentStep == TutorialManager.Step.PickSpoon ||
            tutorial.currentStep == TutorialManager.Step.ScoopDone)
        {
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Kavanozda býrakýldýysa
        if (currentZone == "Jar" && !isFilled)
        {
            isFilled = true;
            tutorial.OnScoopDone();
        }

        // Tabakta býrakýldýysa
        else if (currentZone == "Plate" && isFilled)
        {
            isFilled = false;

            FindObjectOfType<BeadSpawner>().SpawnFixedBeads();
            FindObjectOfType<ResultManager>().ShowResult(5, 3, 4);

            tutorial.OnPour();
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jar"))
            currentZone = "Jar";

        if (other.CompareTag("Plate"))
            currentZone = "Plate";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jar") || other.CompareTag("Plate"))
            currentZone = "";
    }
}