// TutorialLevelManager
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tutorialText;

    private bool isTyping;
    private bool isDisplayingTutorial;
    private Queue<string> tutorialMessages = new Queue<string>();

    private string blinkingCursor = "_";

    void Start()
    {
        if (!GameManager.Instance.isTutorialMode)
        {
            Debug.LogError("Tutorial Mode is OFF, destroying TutorialLevelManager.");
            Destroy(gameObject);
            return;
        }

        if (tutorialText == null)
        {
            Debug.LogError("tutorialText is NULL! Assign the UI Text in Unity.");
            return;
        }

        Debug.Log("Tutorial Mode Activated!");

        tutorialText.gameObject.SetActive(false);
        isDisplayingTutorial = true;

        GameManager.Instance.OnGameStarted += StartTutorialMessages;
    }

    private void StartTutorialMessages()
    {
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = blinkingCursor;

        tutorialMessages.Enqueue("Type the word on a tile to destroy it!");
        tutorialMessages.Enqueue("Don't let tiles fall below the screen!");
        tutorialMessages.Enqueue("Tiles get faster and fall in stronger hordes over time!");
        tutorialMessages.Enqueue("Good luck... ");
        tutorialMessages.Enqueue("...you're gonna need it!");

        StartCoroutine(BlinkCursor());
        StartCoroutine(DisplayTutorialMessages());
    }

    private IEnumerator BlinkCursor()
    {
        bool isCursorVisible = true;

        while (isDisplayingTutorial)
        {
            if (!isTyping)
            {
                tutorialText.text = isCursorVisible ? blinkingCursor : " ";
            }
            isCursorVisible = !isCursorVisible;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator TypeMessage(string message)
    {
        Debug.Log($"Typing message: {message}");

        isTyping = true;
        tutorialText.text = blinkingCursor;

        yield return new WaitForSeconds(0.5f);

        string displayedText = "";
        foreach (char letter in message)
        {
            displayedText += letter;
            tutorialText.text = displayedText + blinkingCursor;
            yield return new WaitForSeconds(0.05f);
        }

        tutorialText.text = displayedText;
        yield return new WaitForSeconds(4f);

        for (int i = displayedText.Length; i >= 0; i--)
        {
            tutorialText.text = displayedText.Substring(0, i) + blinkingCursor;
            yield return new WaitForSeconds(0.03f);
        }

        tutorialText.text = blinkingCursor;
        isTyping = false;
    }

    private IEnumerator DisplayTutorialMessages()
    {
        while (tutorialMessages.Count > 0)
        {
            string message = tutorialMessages.Dequeue();
            yield return StartCoroutine(TypeMessage(message));
            yield return new WaitForSeconds(4f);
        }

        Debug.Log("All tutorial messages displayed!");
        isDisplayingTutorial = false;
        tutorialText.text = "";
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStarted -= StartTutorialMessages;
        }
    }
}
