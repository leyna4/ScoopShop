using UnityEngine;

public class ScoopController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isFilled = false;
    public TutorialManager tutorial;
    private string currentZone = "";

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown - currentStep: " + (tutorial != null ? tutorial.currentStep.ToString() : "No Tutorial"));
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentZone == "Jar" && !isFilled)
        {
            isFilled = true;
            if (tutorial != null)
                tutorial.OnScoopDone();
        }
        else if (currentZone == "Plate" && isFilled)
        {
            isFilled = false;
            BeadSpawner spawner = FindObjectOfType<BeadSpawner>();
            spawner.SpawnFixedBeads();

            ResultManager rm = FindObjectOfType<ResultManager>(true);
            if (rm != null)
                rm.ShowResult(spawner.pink, spawner.red, spawner.blue);
            else
                Debug.LogError("ResultManager bulunamadý!");

            if (tutorial != null)
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
        if (other.CompareTag("Jar")) currentZone = "Jar";
        if (other.CompareTag("Plate")) currentZone = "Plate";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jar") || other.CompareTag("Plate"))
            currentZone = "";
    }
}