// GameManager.cs
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public bool isTutorialMode = false;
    private float gameStartTime;
    public bool gameOver = false;
    public int score;
    private bool gameStarted = false;
    public event Action OnGameStarted;

    void Start()
    {
        score = 0;
        UpdateScoreUI();

        TileScript.currentSpeed = TileScript.initialSpeed;
        TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;
    }

    private void Awake()
    {
        transform.SetParent(null);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu" && scene.name != "LevelSelector")
        {
            score = 0;
            gameStarted = false;
            gameOver = false;
            gameStartTime = 0f;
            isTutorialMode = (scene.name == "TutorialLevel");
            FindScoreText();
            UpdateScoreUI();
            TileScript.currentSpeed = TileScript.initialSpeed;
            TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;
        }
    }

    public void StartGame(bool tutorialMode = false)
    {
        gameStarted = true;
        gameOver = false;
        isTutorialMode = tutorialMode;
        gameStartTime = Time.time;
        TileSpawner.ResetSortingOrder();

        Debug.Log("Game has started!");

        FindScoreText();
        UpdateScoreUI();

        TileScript.currentSpeed = TileScript.initialSpeed;
        TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;

        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        if (gameOver)
            return;
        gameOver = true;
        Debug.Log("Game Over!");

        if (isTutorialMode)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            GameOverManager.Instance.ShowGameOver(score);
        }
    }

    public bool IsGameStarted()
    {
        return gameStarted && !gameOver;
    }

    public void AddScore()
    {
        if (!gameStarted)
        {
            Debug.Log("Game has not started");
            return;
        }
        score++;
        Debug.Log("Score is " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.Log("Score text is null");
        }
    }

    private void FindScoreText()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("ScoreText");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
            Debug.Log("ScoreText found and assigned.");
        }
        else
        {
            Debug.LogError("Score text is null! Ensure it's tagged as 'ScoreText'.");
        }
    }

    public float GetElapsedTime()
    {
        if (!gameStarted)
            return 0;
        return Time.time - gameStartTime;
    }
}
