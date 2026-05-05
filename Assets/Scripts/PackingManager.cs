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

    private List<GameObject> itemsInBox = new List<GameObject>();
    private int pendingScore = 0;
    private bool isCorrectOrder = false;

    void Start()
    {
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
        int packedPink = 0, packedRed = 0, packedBlue = 0;

        foreach (GameObject item in itemsInBox)
        {
            PackingItem pi = item.GetComponent<PackingItem>();
            if (pi == null) continue;
            if (pi.itemType == PackingItem.ItemType.Pink) packedPink++;
            else if (pi.itemType == PackingItem.ItemType.Red) packedRed++;
            else if (pi.itemType == PackingItem.ItemType.Blue) packedBlue++;
        }

        // Doðru mu kontrol et
        isCorrectOrder = (packedPink == requiredPink &&
                          packedRed == requiredRed &&
                          packedBlue == requiredBlue);

        // Tam ücret
        int fullScore = requiredPink * 2 + requiredRed * 3 + requiredBlue * 4;

        // Doðruysa tam, yanlýþsa yarýsý
        pendingScore = isCorrectOrder ? fullScore : fullScore / 2;

        Debug.Log("Doðru: " + isCorrectOrder + " | Skor: " + pendingScore);

        // Kutu kapat
        if (boxOpen != null) boxOpen.SetActive(false);
        if (boxClosed != null) boxClosed.SetActive(true);
        if (shelfArea != null) shelfArea.SetActive(false);

        readyButton.gameObject.SetActive(false);
        if (deliverButton != null)
            deliverButton.gameObject.SetActive(true);

        // Tutorial
        if (tutorialManager != null)
            tutorialManager.OnReadyPressed();
    }

    public void OnDeliverClicked()
    {
        if (boxClosed != null) boxClosed.SetActive(false);
        deliverButton.gameObject.SetActive(false);

        FeedbackManager fm = FindObjectOfType<FeedbackManager>(true);
        if (fm != null)
            fm.ShowFeedback(isCorrectOrder, pendingScore);

        FindObjectOfType<OrderManager>().Deliver(pendingScore, isCorrectOrder);

        itemsInBox.Clear();
    }
}