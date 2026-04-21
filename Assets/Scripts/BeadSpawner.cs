using UnityEngine;

public class BeadSpawner : MonoBehaviour
{
    public GameObject beadPrefab;
    public Transform spawnPoint;

    public int pink = 5;
    public int red = 3;
    public int blue = 4;

    public void SpawnFixedBeads()
    {
        Spawn(pink);
        Spawn(red);
        Spawn(blue);

        FindObjectOfType<BeadCounter>().Count(pink, red, blue);
    }

    void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(beadPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}