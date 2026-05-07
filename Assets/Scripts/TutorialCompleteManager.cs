using UnityEngine;
using UnityEngine.UI;
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

    public void ShowCompleteWithDelay()
    {
        FindObjectOfType<GameManager>().StartCoroutine(DelayedShow());
    }

    IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Panel referansý: " + panel);
        panel.SetActive(true);
        Debug.Log("TutorialCompletePanel açýldý");
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
}