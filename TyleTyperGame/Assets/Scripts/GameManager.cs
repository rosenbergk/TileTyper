// GameManager.cs
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshPro scoreText;
    private float gameStartTime;
    private int score = 0;

    void Start() {
        score = 0;
        UpdateScoreUI();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            gameStartTime = Time.time;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore() {
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
        return Time.time - gameStartTime;
    }
}