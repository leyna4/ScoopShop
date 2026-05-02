using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject scoopArea;
    public GameObject packingArea;

    public void StartPackingPhase()
    {
        scoopArea.SetActive(false);
        packingArea.SetActive(true);
    }
}