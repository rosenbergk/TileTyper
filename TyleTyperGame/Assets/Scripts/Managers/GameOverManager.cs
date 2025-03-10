// GameOverManager.cs
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ShowGameOver(int finalScore)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        if (SceneManager.GetActiveScene().name != "TutorialLevel")
        {
            finalScoreText.text = finalScore.ToString();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GoToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
