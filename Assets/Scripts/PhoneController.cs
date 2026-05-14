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
            LevelConfig config = FindObjectOfType<LevelManager>()?.GetCurrentConfig();
            int scoopCount = 1;
            if (config != null)
                scoopCount = Random.Range(config.minScoops, config.maxScoops + 1);

            messagePanel.SetActive(true);
            messageText.text = "Customer: " + scoopCount + " Scoop" + (scoopCount > 1 ? "s" : "");

            // Kaþýk sayýsýný ScoopController'a ilet
            ScoopController sc = FindObjectOfType<ScoopController>();
            if (sc != null) sc.SetRequiredScoops(scoopCount);

            TimerManager timer = FindObjectOfType<TimerManager>();
            if (timer != null && config != null && config.hasTimer)
                timer.StartOrderTimer(config.timerSeconds);

            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager != null) tutorialManager.OnPhoneOpened();
        }
        // Yeni sipariþ baþýnda boncuk sayaçlarýný sýfýrla
        FindObjectOfType<BeadSpawner>().ResetCounts();

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