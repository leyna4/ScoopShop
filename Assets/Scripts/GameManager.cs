using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ScoopArea;
    public GameObject PackingArea;
    public BeadSpawner beadSpawner;
    public ShelfSpawner shelfSpawner;
    public PackingManager packingManager;
    public GameObject shelfArea;
    public GameObject boxOpen;
    public GameObject resultText;
    public void StartPackingPhase()
    {
        if (resultText != null) resultText.SetActive(true);
        Debug.Log("StartPackingPhase çağrıldı");

        beadSpawner.ClearBeads();
        ScoopArea.SetActive(false);
        PackingArea.SetActive(true);

        if (shelfArea != null) shelfArea.SetActive(true);
        if (boxOpen != null) boxOpen.SetActive(true);

        PackingManager pm = packingManager;
        pm.readyButton.gameObject.SetActive(true);
        pm.readyButton.interactable = false;
        pm.deliverButton.gameObject.SetActive(false);

        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm == null) { Debug.LogError("ResultManager null!"); return; }
        if (shelfSpawner == null) { Debug.LogError("ShelfSpawner null!"); return; }

        shelfSpawner.SpawnItems(rm.pink, rm.red, rm.blue);

        pm.requiredPink = rm.pink;
        pm.requiredRed = rm.red;
        pm.requiredBlue = rm.blue;
    }

    public void StartScoopPhase()
    {
        PackingArea.SetActive(false);
        ScoopArea.SetActive(true);
        if (resultText != null) resultText.SetActive(false);
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    
}