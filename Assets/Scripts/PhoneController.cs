using UnityEngine;
using TMPro;
using System.Collections;

public class PhoneController : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public GameObject feedbackPanel;

    private bool isShaking = false;
    private bool isClickable = false;
    private bool isFeedbackMode = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(StartRinging());
    }

    IEnumerator StartRinging()
    {
        yield return new WaitForSeconds(0.5f);
        isShaking = true;
        isClickable = true;
    }

    void Update()
    {
        if (isShaking)
            transform.position = startPos + Random.insideUnitSphere * 0.05f;
    }

    void OnMouseDown()
    {
        if (!isClickable) return;

        isShaking = false;
        isClickable = false;
        transform.position = startPos;

        if (isFeedbackMode)
        {
            // Feedback modunda feedback paneli aç
            isFeedbackMode = false;
            feedbackPanel.SetActive(true);
        }
        else
        {
            // Normal modda sipariþ paneli aç
            messagePanel.SetActive(true);
            messageText.text = "Customer: 1 Scoop";
            FindObjectOfType<TutorialManager>().OnPhoneOpened();
        }
    }

    public void RingAgain(bool feedbackMode)
    {
        isFeedbackMode = feedbackMode;
        isShaking = false;
        isClickable = false;
        StartCoroutine(DelayedRing());
    }

    IEnumerator DelayedRing()
    {
        yield return new WaitForSeconds(1f);
        startPos = transform.position;
        isShaking = true;
        isClickable = true;

        if (isFeedbackMode)
            FindObjectOfType<TutorialManager>().OnFeedbackCall();
        else
            FindObjectOfType<TutorialManager>().OnNewOrder();
    }
}