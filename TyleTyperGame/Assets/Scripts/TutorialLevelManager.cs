// TutorialLevelManager
using UnityEngine;
using TMPro;

public class TutorialLevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    
    private bool initialMessageShown;
    private bool secondMessageShown;
    private bool thirdMessageShown;

    void Start()
    {
        initialMessageShown = false;
        secondMessageShown = false;
        thirdMessageShown = false;
        tutorialText.text = "";

        CountdownManager countdownManager = FindAnyObjectByType<CountdownManager>();
        if (countdownManager != null)
        {
            countdownManager.StartCountdown();
        }
        
        Invoke("ShowInitialMessage", 5f);
    }

    void Update()
    {
        if (!initialMessageShown && GameManager.Instance.score >= 3)
        {
            tutorialText.text = "";
            initialMessageShown = true;
        }
        else if (!secondMessageShown && GameManager.Instance.score >= 5 && GameManager.Instance.score <= 8)
        {
            tutorialText.text = "Tiles get faster and spawn more frequently over time!";
            secondMessageShown = true;
        } else if (!thirdMessageShown && GameManager.Instance.score >= 8 && GameManager.Instance.score <= 10) {
            tutorialText.text = "Good luck... you're gonna need it";
            thirdMessageShown = true;
        }

        if (initialMessageShown && secondMessageShown && thirdMessageShown && GameManager.Instance.score >= 12) {
            tutorialText.text = "";
        }
    }

    void ShowInitialMessage()
    {
        tutorialText.text = "Type the word on a tile to destroy it!\n\nDon't let tiles fall below the screen!";
    }
}