using UnityEngine;
using System.Collections.Generic;

public class BeadSpawner : MonoBehaviour
{
    public GameObject beadPrefab;
    public Transform spawnPoint;
    public int pink = 5;
    public int red = 3;
    public int blue = 4;

    private List<GameObject> spawnedBeads = new List<GameObject>();

    public void SpawnFixedBeads()
    {
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
            GameObject bead = Instantiate(
                beadPrefab,
                spawnPoint.position,
                Quaternion.identity);
            spawnedBeads.Add(bead);
        }
    }

    public void ClearBeads()
    {
        foreach (GameObject bead in spawnedBeads)
        {
            if (bead != null)
                Destroy(bead);
        }
        spawnedBeads.Clear();
    }
}