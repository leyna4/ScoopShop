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

        // Timer'ı durdur
        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null) timer.StopTimer();

        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm != null) rm.HidePanel();
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1Scene");
    }
    public void ResetScene()
    {
        // PackingArea kapat
        if (PackingArea != null) PackingArea.SetActive(false);

        // ScoopArea aç
        if (ScoopArea != null) ScoopArea.SetActive(true);

        // Beadleri temizle
        beadSpawner.ClearBeads();

        // ShelfArea kapat
        if (shelfArea != null) shelfArea.SetActive(false);

        // Box sıfırla
        if (boxOpen != null) boxOpen.SetActive(false);

        // Tüm panelleri kapat
        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm != null) rm.HidePanel();

        // PackingManager sıfırla
        if (packingManager != null)
        {
            packingManager.readyButton.gameObject.SetActive(true);
            packingManager.readyButton.interactable = false;
            packingManager.deliverButton.gameObject.SetActive(false);
        }

        // Timer durdur
        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null) timer.StopTimer();

        // MessagePanel kapat
        GameObject mp = GameObject.Find("MessagePanel");
        if (mp != null) mp.SetActive(false);

        // ResultPanel kapat
        GameObject rp = GameObject.Find("ResultPanel");
        if (rp != null) rp.SetActive(false);

        // FeedbackPanel kapat
        FeedbackManager fm = FindObjectOfType<FeedbackManager>(true);
        if (fm != null) fm.GetPanel().SetActive(false);

        // ResultText gizle
        if (resultText != null) resultText.SetActive(false);

        // ReadyButton gizle
        if (packingManager != null)
            packingManager.readyButton.gameObject.SetActive(false);
    }

}