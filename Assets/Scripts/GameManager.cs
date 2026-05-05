using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ScoopArea;
    public GameObject PackingArea;
    public BeadSpawner beadSpawner;
    public ShelfSpawner shelfSpawner;
    public PackingManager packingManager;

    public void StartPackingPhase()
    {
        Debug.Log("StartPackingPhase çağrıldı");

        beadSpawner.ClearBeads();

        ScoopArea.SetActive(false);
        PackingArea.SetActive(true);

        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm == null) { Debug.LogError("ResultManager null!"); return; }
        if (shelfSpawner == null) { Debug.LogError("ShelfSpawner null!"); return; }
        if (packingManager == null) { Debug.LogError("PackingManager null!"); return; }

        shelfSpawner.SpawnItems(rm.pink, rm.red, rm.blue);

        packingManager.requiredPink = rm.pink;
        packingManager.requiredRed = rm.red;
        packingManager.requiredBlue = rm.blue;

        TutorialManager tm = FindObjectOfType<TutorialManager>();
        if (tm != null)
            tm.OnPackingStarted();
        else
            Debug.LogError("TutorialManager null!");
    }
    public void StartLevel1()
    {
        Debug.Log("Level 1 başlıyor!");
        // Şimdilik sadece log, level sistemi sonra gelecek
    }
}