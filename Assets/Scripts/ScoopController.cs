using UnityEngine;

public class ScoopController : MonoBehaviour
{
    private bool isDragging = false;
    private bool isFilled = false;
    public TutorialManager tutorial;
    private string currentZone = "";

    private int requiredScoops = 1;
    private int completedScoops = 0;

    [Header("Görsel")]
    public SpriteRenderer scoopRenderer;
    public Sprite emptySprite;
    public Sprite fullSprite;
    public GameObject beadOnScoop; // kaþýk üstündeki boncuk objesi

    void Start()
    {
        SetEmpty();
    }

    void SetEmpty()
    {
        if (scoopRenderer != null && emptySprite != null)
            scoopRenderer.sprite = emptySprite;
        if (beadOnScoop != null)
            beadOnScoop.SetActive(false);
    }

    void SetFull()
    {
        if (scoopRenderer != null && fullSprite != null)
            scoopRenderer.sprite = fullSprite;
        if (beadOnScoop != null)
            beadOnScoop.SetActive(true);
    }

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
            SetFull();
            if (tutorial != null) tutorial.OnScoopDone();
        }
        else if (currentZone == "Plate" && isFilled)
        {
            isFilled = false;
            SetEmpty();
            completedScoops++;

            BeadSpawner spawner = FindObjectOfType<BeadSpawner>();
            spawner.SpawnFixedBeads();

            Debug.Log("Kaþýk tamamlandý: " + completedScoops + "/" + requiredScoops);

            if (completedScoops >= requiredScoops)
            {
                completedScoops = 0;
                ResultManager rm = FindObjectOfType<ResultManager>(true);
                if (rm != null)
                    rm.ShowResult(spawner.pink, spawner.red, spawner.blue);

                if (tutorial != null) tutorial.OnPour();
            }
            else
            {
                Debug.Log("Daha " + (requiredScoops - completedScoops) + " kaþýk gerekiyor!");
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