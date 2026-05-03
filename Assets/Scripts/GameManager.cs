using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ScoopArea;
    public GameObject PackingArea;

    public void StartPackingPhase()
    {
        Debug.Log("StartPackingPhase çaðrýldý");
        Debug.Log("PackingArea: " + PackingArea);
        Debug.Log("ScoopArea: " + ScoopArea);

        // Bead'leri temizle
        FindObjectOfType<BeadSpawner>().ClearBeads();

        ScoopArea.SetActive(false);
        PackingArea.SetActive(true);

        ResultManager rm = FindObjectOfType<ResultManager>(true);
        ShelfSpawner spawner = FindObjectOfType<ShelfSpawner>();
        PackingManager pm = FindObjectOfType<PackingManager>(true);

        spawner.SpawnItems(rm.pink, rm.red, rm.blue);

        pm.requiredPink = rm.pink;
        pm.requiredRed = rm.red;
        pm.requiredBlue = rm.blue;

        TutorialManager tm = FindObjectOfType<TutorialManager>();
        if (tm != null)
            tm.OnPackingStarted();
        else
            Debug.LogError("TutorialManager bulunamadý!");
    }
}