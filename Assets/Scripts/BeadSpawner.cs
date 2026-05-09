using UnityEngine;
using System.Collections.Generic;

public class BeadSpawner : MonoBehaviour
{
    public GameObject beadPrefab;
    public Transform spawnPoint;

    [Header("Level Limitleri")]
    public int minPerType = 1;
    public int maxPerType = 5;
    public int activeBoncukTypes = 1;

    [HideInInspector] public int pink;
    [HideInInspector] public int red;
    [HideInInspector] public int blue;

    private List<GameObject> spawnedBeads = new List<GameObject>();

    public void SpawnFixedBeads()
    {
        pink = activeBoncukTypes >= 1 ? Random.Range(minPerType, maxPerType + 1) : 0;
        red = activeBoncukTypes >= 2 ? Random.Range(minPerType, maxPerType + 1) : 0;
        blue = activeBoncukTypes >= 3 ? Random.Range(minPerType, maxPerType + 1) : 0;

        Debug.Log("Sipariþ: " + pink + " pink, " + red + " red, " + blue + " blue");

        SpawnBeads(pink);
        SpawnBeads(red);
        SpawnBeads(blue);

        BeadCounter counter = FindObjectOfType<BeadCounter>();
        if (counter != null)
            counter.Count(pink, red, blue);
    }

    void SpawnBeads(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bead = Instantiate(beadPrefab, spawnPoint.position, Quaternion.identity);
            spawnedBeads.Add(bead);
        }
    }

    public void ClearBeads()
    {
        foreach (GameObject bead in spawnedBeads)
            if (bead != null) Destroy(bead);
        spawnedBeads.Clear();
    }
}