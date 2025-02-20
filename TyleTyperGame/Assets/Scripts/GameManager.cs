// GameManager.cs
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshPro scoreText;
    private float gameStartTime;
    private int score = 0;
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
        gameStartTime = Time.time;
        OnGameStarted?.Invoke();
    }

    public bool IsGameStarted() {
        return gameStarted;
    }

    public void AddScore() {
        if (!gameStarted) return;
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI() {
        if (scoreText != null) {
            scoreText.text = score.ToString();
        } 
    }
    public float GetElapsedTime()
    {
        if (!gameStarted) return 0;
        return Time.time - gameStartTime;
    }
}