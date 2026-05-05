using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialCompleteManager : MonoBehaviour
{
    public GameObject panel;
    public Button startButton;

    void Start()
    {
        panel.SetActive(false);
        startButton.onClick.AddListener(OnStartClicked);
    }

    public void ShowTutorialComplete()
    {
        panel.SetActive(true);
    }

    void OnStartClicked()
    {
        panel.SetActive(false);
        FindObjectOfType<GameManager>().StartLevel1();
    }
    void OnClose()
    {
        panel.SetActive(false);
        StartCoroutine(ShowCompleteDelayed());
    }

    IEnumerator ShowCompleteDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<TutorialCompleteManager>().ShowTutorialComplete();
    }
}