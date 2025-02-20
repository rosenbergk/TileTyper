using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class StartMenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public TextMeshProUGUI startText;
    private bool gameStarted = false;


    private void Start()
    {
        StartCoroutine(BlinkCursor());
    }
    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space)) 
        {
            gameStarted = true;
            startMenu.SetActive(false); 
            StartCoroutine(LoadColorLevelAndStartCountdown());
        }
    }


    private System.Collections.IEnumerator LoadColorLevelAndStartCountdown()
    {
        SceneManager.LoadScene("ColorLevel", LoadSceneMode.Additive);
        yield return null;

        CountdownManager countdownManager = FindAnyObjectByType<CountdownManager>();

        if (countdownManager != null)
        {
            countdownManager.StartCountdown(); 
        }
    }

    private IEnumerator BlinkCursor()
    {
        bool isCursorVisible = true;

        while (true)
        {
            if (startText != null)
            {
                string baseText = "Press SPACE to Start";
                startText.text = baseText + (isCursorVisible ? "_" : " ");
            }

            isCursorVisible = !isCursorVisible;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
