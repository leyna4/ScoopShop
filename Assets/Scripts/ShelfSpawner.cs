using UnityEngine;
using System.Collections.Generic;

public class ShelfSpawner : MonoBehaviour
{
    [Header("Prefablar")]
    public GameObject pinkItemPrefab;
    public GameObject redItemPrefab;
    public GameObject blueItemPrefab;

    [Header("Raf Satirlari")]
    public RectTransform shelf1Row;
    public RectTransform shelf2Row;
    public RectTransform shelf3Row;

    [Header("Ayarlar")]
    public float itemSpacing = 55f;
    public int itemsPerRow = 5;
    public int totalItems = 15;

    private List<GameObject> spawnedItems = new List<GameObject>();

    public void SpawnItems(int pink, int red, int blue)
    {
        Debug.Log("SpawnItems çaðrýldý: " + pink + " " + red + " " + blue);

        foreach (GameObject item in spawnedItems)
            Destroy(item);
        spawnedItems.Clear();

        Debug.Log("Shelf1Row: " + shelf1Row);
        Debug.Log("Shelf2Row: " + shelf2Row);
        Debug.Log("Shelf3Row: " + shelf3Row);

        SpawnToShelf(pinkItemPrefab, shelf1Row);
        SpawnToShelf(redItemPrefab, shelf2Row);
        SpawnToShelf(blueItemPrefab, shelf3Row);
    }

    void SpawnToShelf(GameObject prefab, RectTransform parent)
    {
        for (int i = 0; i < totalItems; i++)
        {
            int col = i % itemsPerRow;

            GameObject item = Instantiate(prefab, parent);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0.5f);
            rt.anchorMax = new Vector2(0, 0.5f);
            rt.pivot = new Vector2(0, 0.5f);
            rt.sizeDelta = new Vector2(50, 50);
            rt.anchoredPosition = new Vector2(col * itemSpacing, 0);
            item.transform.SetSiblingIndex(0);
            spawnedItems.Add(item);
        }
    }
}