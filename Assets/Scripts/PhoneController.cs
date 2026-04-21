using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private bool isShaking = true;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isShaking)
        {
            transform.position = startPos + Random.insideUnitSphere * 0.1f;
        }
    }

    void OnMouseDown()
    {
        isShaking = false;
        transform.position = startPos;

        messagePanel.SetActive(true);
        messageText.text = "Customer: 1 Scoop";

        FindObjectOfType<TutorialManager>().OnPhoneOpened();
    }
}