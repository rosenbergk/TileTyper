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
    public bool gameOver = false;
    public int score;
    public event Action OnGameStarted;

    private float gameStartTime;
    private bool gameStarted = false;

    void Start()
    {
        score = 0;
        UpdateScoreUI();

        TileScript.currentSpeed = TileScript.initialSpeed;
        TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;
    }

    public void Awake()
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

    public void StartGame(bool tutorialMode = false)
    {
        gameStarted = true;
        gameOver = false;
        isTutorialMode = tutorialMode;
        gameStartTime = Time.time;
        TileSpawner.ResetSortingOrder();

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
            return;
        }

        score++;
        UpdateScoreUI();
    }

    public float GetElapsedTime()
    {
        if (!gameStarted)
            return 0;
        return Time.time - gameStartTime;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void FindScoreText()
    {
        GameObject scoreObject = GameObject.FindGameObjectWithTag("ScoreText");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
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
}
