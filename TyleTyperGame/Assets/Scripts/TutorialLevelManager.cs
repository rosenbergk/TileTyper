// TutorialLevelManager
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialLevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    
    private bool isTyping;
    private bool isDisplayingTutorial;
    private Queue<string> tutorialMessages = new Queue<string>();

    private string blinkingCursor = "_";

    void Start()
    {
        if (!GameManager.Instance.isTutorialMode)
        {
            Destroy(gameObject);
            return;
        }
        tutorialText.text = blinkingCursor;
        isDisplayingTutorial = true;

        tutorialMessages.Enqueue("Type the word on a tile to destroy it!");
        tutorialMessages.Enqueue("Don't let tiles fall below the screen!");
        tutorialMessages.Enqueue("Tiles get faster and spawn more frequently over time!");
        tutorialMessages.Enqueue("Good luck... you're gonna need it!");

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

        yield return new WaitForSeconds(0.5f); // ✅ Delay before typing begins

        string displayedText = "";
        foreach (char letter in message)
        {
            displayedText += letter;
            tutorialText.text = displayedText + blinkingCursor;
            yield return new WaitForSeconds(0.05f);
        }

        tutorialText.text = displayedText;
        yield return new WaitForSeconds(3f); // ✅ Extended delay before deleting

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
        if (tutorialMessages.Count == 0)
        {
            Debug.LogWarning("No tutorial messages found! Check tutorialMessages queue.");
        }

        while (tutorialMessages.Count > 0)
        {
            string message = tutorialMessages.Dequeue();
            yield return StartCoroutine(TypeMessage(message));
            yield return new WaitForSeconds(2f); // ✅ Extra delay between messages
        }

        Debug.Log("All tutorial messages displayed!");
        isDisplayingTutorial = false;
        tutorialText.text = ""; // ✅ Remove blinking cursor when done
    }
}