using UnityEngine;
using System.Collections.Generic;

public class ShelfSpawner : MonoBehaviour
{
    [Header("Prefablar")]
    public GameObject pinkItemPrefab;
    public GameObject redItemPrefab;
    public GameObject blueItemPrefab;

    [Header("Spawn Noktaları")]
    public Transform shelf1Slots;
    public Transform shelf2Slots;
    public Transform shelf3Slots;

    [Header("Düzen")]
    public float itemSpacing = 70f;
    public int itemsPerRow = 5;
    public int totalItems = 15;

    private List<GameObject> spawnedItems = new List<GameObject>();

    public void SpawnItems(int pink, int red, int blue)
    {
        foreach (GameObject item in spawnedItems)
            Destroy(item);
        spawnedItems.Clear();

        SpawnRow(pinkItemPrefab, shelf1Slots);
        SpawnRow(redItemPrefab, shelf2Slots);
        SpawnRow(blueItemPrefab, shelf3Slots);
    }

    void SpawnRow(GameObject prefab, Transform parent)
    {
        for (int i = 0; i < totalItems; i++)
        {
            GameObject item = Instantiate(prefab, parent); // 🔥 BURASI KRİTİK

            RectTransform rt = item.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.anchoredPosition = Vector2.zero; // Grid kullanıyorsan bu yeterli

            spawnedItems.Add(item);
        }
    }
}