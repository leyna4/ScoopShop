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

    public void StartPackingPhase()
    {
        Debug.Log("StartPackingPhase çağrıldı");

        beadSpawner.ClearBeads();
        ScoopArea.SetActive(false);
        PackingArea.SetActive(true);

        if (shelfArea != null)
            shelfArea.SetActive(true);
        else
            Debug.LogError("ShelfArea null! Inspector'da bağla.");

        if (boxOpen != null)
            boxOpen.SetActive(true);
        else
            Debug.LogError("BoxOpen null! Inspector'da bağla.");

        PackingManager pm = packingManager;
        pm.readyButton.gameObject.SetActive(true);
        pm.readyButton.interactable = false;
        pm.deliverButton.gameObject.SetActive(false);

        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm == null) { Debug.LogError("ResultManager null!"); return; }

        shelfSpawner.SpawnItems(rm.pink, rm.red, rm.blue);

        pm.requiredPink = rm.pink;
        pm.requiredRed = rm.red;
        pm.requiredBlue = rm.blue;
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void StartScoopPhase()
    {
        PackingArea.SetActive(false);
        ScoopArea.SetActive(true);
    }
}