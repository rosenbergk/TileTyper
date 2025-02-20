// GameManager.cs
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshPro scoreText;
    private float gameStartTime;
    private bool gameOver = false;
    private int score;
    private bool gameStarted = false;
    public event Action OnGameStarted;

    void Start() {
        score = 0;
        UpdateScoreUI();
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

        FindScoreText();
        UpdateScoreUI();

        TileScript.currentSpeed = TileScript.initialSpeed;
        TileSpawner.currentSpawnInterval = TileSpawner.initialSpawnInterval;

        OnGameStarted?.Invoke();
    }

    public void GameOver()
    {
        if (gameOver) return;
        score = 0;
        gameOver = true;
        Debug.Log("Game Over! Returning to menu...");
        Invoke(nameof(ReturnToMainMenu), 0.1f);
    }

    private void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
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
            scoreText = scoreObject.GetComponent<TextMeshPro>();
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