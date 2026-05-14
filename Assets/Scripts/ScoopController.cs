using UnityEngine;

public class ScoopController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isFilled = false;
    public TutorialManager tutorial;
    private string currentZone = "";

    private int requiredScoops = 1;
    private int completedScoops = 0;

    public void SetRequiredScoops(int count)
    {
        requiredScoops = count;
        completedScoops = 0;
        Debug.Log("Gereken kaþýk: " + requiredScoops);
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentZone == "Jar" && !isFilled)
        {
            isFilled = true;
            if (tutorial != null) tutorial.OnScoopDone();
        }
        else if (currentZone == "Plate" && isFilled)
        {
            isFilled = false;
            completedScoops++;

            BeadSpawner spawner = FindObjectOfType<BeadSpawner>();
            spawner.SpawnFixedBeads();

            Debug.Log("Kaþýk tamamlandý: " + completedScoops + "/" + requiredScoops);

            if (completedScoops >= requiredScoops)
            {
                // Tüm kaþýklar tamamlandý, result paneli göster
                completedScoops = 0;
                ResultManager rm = FindObjectOfType<ResultManager>(true);
                if (rm != null)
                    rm.ShowResult(spawner.pink, spawner.red, spawner.blue);

                if (tutorial != null) tutorial.OnPour();
            }
            else
            {
                // Daha fazla kaþýk gerekiyor, ok butonu çýkmasýn
                Debug.Log("Daha " + (requiredScoops - completedScoops) + " kaþýk daha gerekiyor!");
            }
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