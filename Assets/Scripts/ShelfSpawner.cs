using UnityEngine;
using System.Collections.Generic;

public class ShelfSpawner : MonoBehaviour
{
    [Header("Prefablar")]
    public GameObject pinkItemPrefab;
    public GameObject redItemPrefab;
    public GameObject blueItemPrefab;

    [Header("Spawn Noktalarý")]
    public Transform shelf1Slots;
    public Transform shelf2Slots;
    public Transform shelf3Slots;

    [Header("Düzen")]
    public float itemSpacing = 70f;   // yatay aralýk
    public float rowSpacing = 70f;    // dikey aralýk (sýra aralýðý)
    public int itemsPerRow = 5;       // her sýrada max item
    public int totalItems = 15;       // toplam spawn sayýsý

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
            int col = i % itemsPerRow;
            int row = i / itemsPerRow;

            float x = col * itemSpacing;
            float y = -row * rowSpacing;

            GameObject item = Instantiate(prefab, parent, false); // false = world position'ý koru
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(x, y);
            rt.localScale = Vector3.one; // scale bozulmasýn
            spawnedItems.Add(item);
        }
    }
}