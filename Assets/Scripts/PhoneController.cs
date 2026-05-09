using UnityEngine;
using TMPro;
using System.Collections;

public class PhoneController : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private bool isShaking = true;
    private bool isClickable = true;
    private bool isFeedbackMode = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
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
            isFeedbackMode = false;
            FeedbackManager fm = FindObjectOfType<FeedbackManager>(true);
            if (fm != null)
                fm.ShowFeedback();
            else
                Debug.LogError("FeedbackManager bulunamadý!");
        }
        else
        {
            messagePanel.SetActive(true);
            messageText.text = "Customer: 1 Scoop";
            TutorialManager tm = FindObjectOfType<TutorialManager>();
            if (tm != null) tm.OnPhoneOpened();
        }
    }

    public void RingAgain(bool feedbackMode)
    {
        isFeedbackMode = feedbackMode;
        StartCoroutine(DelayedRing());
    }

    IEnumerator DelayedRing()
    {
        yield return new WaitForSeconds(1f);
        startPos = transform.position;
        isShaking = true;
        isClickable = true;
    }
}