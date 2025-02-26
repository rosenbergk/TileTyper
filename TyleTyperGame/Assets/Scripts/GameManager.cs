// GameManager.cs
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    private float gameStartTime;
    public bool gameOver = false;
    public int score;
    private bool gameStarted = false;
    public event Action OnGameStarted;

    void Start() {
        score = 0;
        UpdateScoreUI();


        CountdownManager countdownManager = FindAnyObjectByType<CountdownManager>();
        if (countdownManager != null)
        {
            countdownManager.StartCountdown();
        }

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
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartGame() {
        gameStarted = true;
        gameOver = false;
        gameStartTime = Time.time;

        Debug.Log("Game has started!");

        FindScoreText();
        UpdateScoreUI();

        TileScript.currentSpeed = TileScript.initialSpeed;
        TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;

        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        Debug.Log("Game Over!");

        GameOverManager.Instance.ShowGameOver(score);
    }
    
    public bool IsGameStarted() {
        return gameStarted && !gameOver;
    }

    public void AddScore() {
        
        if (!gameStarted) {
            Debug.Log("Game has not started");
            return;
        }

        score++;
        Debug.Log("Score is " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI() {
        if (scoreText != null) {
            scoreText.text = score.ToString();
        } else {
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
        if (!gameStarted) return 0;
        return Time.time - gameStartTime;
    }
}