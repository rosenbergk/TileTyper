// TutorialLevelManager
using UnityEngine;
using TMPro;

public class TutorialLevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    
    private bool initialMessageShown;
    private bool secondMessageShown;

    void Start()
    {
        initialMessageShown = false;
        secondMessageShown = false;
        tutorialText.text = "";
        Invoke("ShowInitialMessage", 5f);
    }

    void Update()
    {
        if (!initialMessageShown && GameManager.Instance.score >= 3)
        {
            tutorialText.text = "";
            initialMessageShown = true;
        }
        else if (!secondMessageShown && GameManager.Instance.score >= 5)
        {
            tutorialText.text = "Tiles get faster and spawn more frequently over time!";
            secondMessageShown = true;
        }
    }

    void ShowInitialMessage()
    {
        tutorialText.text = "Type the word on a tile to destroy it!\nDon't let tiles fall below the screen!";
    }
}