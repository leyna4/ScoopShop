using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PackingManager : MonoBehaviour
{
    [Header("Sipariþ Bilgisi")]
    public int requiredPink;
    public int requiredRed;
    public int requiredBlue;

    [Header("Kutu")]
    public GameObject boxDropZone;
    public List<GameObject> itemsInBox = new List<GameObject>();

    [Header("Buton")]
    public Button readyButton;

    [Header("Tutorial")]
    public TutorialManager tutorialManager;

    void Start()
    {
        readyButton.interactable = false;
    }

    // Ürün kutuya eklenince çaðrýlýr
    public void ItemAddedToBox(GameObject item)
    {
        itemsInBox.Add(item);
        Debug.Log("Kutuya eklendi: " + item.name + " | Toplam: " + itemsInBox.Count);

        if (tutorialManager != null)
            tutorialManager.OnItemPacked();

        // En az 1 ürün varsa butonu aktif et
        if (itemsInBox.Count > 0)
            readyButton.interactable = true;
    }

    // Hazýr butonuna basýnca
    public void OnReadyClicked()
    {
        int packedPink = 0;
        int packedRed = 0;
        int packedBlue = 0;

        foreach (GameObject item in itemsInBox)
        {
            PackingItem pi = item.GetComponent<PackingItem>();
            if (pi == null) continue;

            if (pi.itemType == PackingItem.ItemType.Pink) packedPink++;
            else if (pi.itemType == PackingItem.ItemType.Red) packedRed++;
            else if (pi.itemType == PackingItem.ItemType.Blue) packedBlue++;
        }

        // Puan hesapla
        int score = CalculateScore(packedPink, packedRed, packedBlue);
        Debug.Log("SKOR: " + score);

        FindObjectOfType<OrderManager>().Deliver(score);

        if (tutorialManager != null)
            tutorialManager.OnOrderSent();
    }

    int CalculateScore(int p, int r, int b)
    {
        int maxScore = requiredPink * 2 + requiredRed * 3 + requiredBlue * 4;
        int penalty = 0;

        // Eksik ürün cezasý
        int missingPink = Mathf.Max(0, requiredPink - p);
        int missingRed = Mathf.Max(0, requiredRed - r);
        int missingBlue = Mathf.Max(0, requiredBlue - b);

        penalty += missingPink * 2;
        penalty += missingRed * 3;
        penalty += missingBlue * 4;

        // Fazla ürün cezasý
        int extraPink = Mathf.Max(0, p - requiredPink);
        int extraRed = Mathf.Max(0, r - requiredRed);
        int extraBlue = Mathf.Max(0, b - requiredBlue);

        penalty += extraPink * 1;
        penalty += extraRed * 1;
        penalty += extraBlue * 1;

        return Mathf.Max(0, maxScore - penalty);
    }
}