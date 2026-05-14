using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PackingManager : MonoBehaviour
{
    [Header("Siparis Bilgisi")]
    public int requiredPink;
    public int requiredRed;
    public int requiredBlue;

    [Header("Kutu")]
    public GameObject boxOpen;
    public GameObject boxClosed;
    public GameObject shelfArea;

    [Header("Butonlar")]
    public Button readyButton;
    public Button deliverButton;

    [Header("Tutorial")]
    public TutorialManager tutorialManager;

    [Header("Level Config")]
    public LevelConfig levelConfig;

    private List<GameObject> itemsInBox = new List<GameObject>();
    private int pendingScore = 0;
    private bool isCorrectOrder = false;

    void Start()
    {
        readyButton.gameObject.SetActive(true);
        readyButton.interactable = false;
        if (deliverButton != null)
            deliverButton.gameObject.SetActive(false);
    }

    public void ItemAddedToBox(GameObject item)
    {
        itemsInBox.Add(item);
        readyButton.interactable = true;
        if (tutorialManager != null)
            tutorialManager.OnItemPacked();
    }

    public void OnReadyClicked()
    {
        FindObjectOfType<GameManager>().resultText.SetActive(false);
        ResultManager rm = FindObjectOfType<ResultManager>(true);
        if (rm != null) rm.HidePanel();


        int packedPink = 0, packedRed = 0, packedBlue = 0;

        foreach (GameObject item in itemsInBox)
        {
            PackingItem pi = item.GetComponent<PackingItem>();
            if (pi == null) continue;
            if (pi.itemType == PackingItem.ItemType.Pink) packedPink++;
            else if (pi.itemType == PackingItem.ItemType.Red) packedRed++;
            else if (pi.itemType == PackingItem.ItemType.Blue) packedBlue++;
        }

        isCorrectOrder = (packedPink == requiredPink &&
                          packedRed == requiredRed &&
                          packedBlue == requiredBlue);

        pendingScore = CalculateScore(packedPink, packedRed, packedBlue);

        if (boxOpen != null) boxOpen.SetActive(false);
        if (boxClosed != null) boxClosed.SetActive(true);
        if (shelfArea != null) shelfArea.SetActive(false);

        readyButton.gameObject.SetActive(false);
        if (deliverButton != null)
            deliverButton.gameObject.SetActive(true);

        if (tutorialManager != null)
            tutorialManager.OnReadyPressed();
    }

    public void OnDeliverClicked()
    {
        if (boxClosed != null) boxClosed.SetActive(false);
        deliverButton.gameObject.SetActive(false);

        FindObjectOfType<OrderManager>().Deliver(pendingScore, isCorrectOrder);

        itemsInBox.Clear();
        TimerManager tm = FindObjectOfType<TimerManager>();
        if (tm != null) tm.StopTimer();
    }

    int CalculateScore(int p, int r, int b)
    {
        int pinkValue = levelConfig != null ? levelConfig.hairClipValue : 2;
        int redValue = levelConfig != null ? levelConfig.wetWipeValue : 3;
        int blueValue = levelConfig != null ? levelConfig.nailPolishValue : 4;

        int maxScore = requiredPink * pinkValue +
                       requiredRed * redValue +
                       requiredBlue * blueValue;

        int penalty = 0;
        penalty += Mathf.Max(0, requiredPink - p) * pinkValue;
        penalty += Mathf.Max(0, requiredRed - r) * redValue;
        penalty += Mathf.Max(0, requiredBlue - b) * blueValue;
        penalty += Mathf.Max(0, p - requiredPink) * (pinkValue / 2);
        penalty += Mathf.Max(0, r - requiredRed) * (redValue / 2);
        penalty += Mathf.Max(0, b - requiredBlue) * (blueValue / 2);

        return Mathf.Max(0, maxScore - penalty);
    }
    public void ForceDeliver()
    {
        // Süre bitince ne varsa teslim et, 0 puan
        if (boxClosed != null) boxClosed.SetActive(false);
        if (deliverButton != null) deliverButton.gameObject.SetActive(false);
        if (readyButton != null) readyButton.gameObject.SetActive(false);

        FindObjectOfType<OrderManager>().Deliver(0, false);
        itemsInBox.Clear();
    }
}