// TileScript.cs
using System;
using UnityEngine;
using TMPro;

public class TileScript : MonoBehaviour
{
    public String tileWord;
    public static float currentSpeed;
    public static float initialSpeed = 0.5f;

    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float growthRate = 0.02f;
    [SerializeField] private float fluctationMagnitude = 0.4f;
    [SerializeField] private float noiseSpeed = 0.04f;

    private TextMeshPro textMesh;
    private float noiseOffset;

    public void SetTileWord(String word) {
        tileWord = word;
        if (textMesh != null) {
            textMesh.text = word;
        }
    }

    private void Awake() {
        textMesh = GetComponentInChildren<TextMeshPro>();
        noiseOffset = UnityEngine.Random.Range(0f, 100f);
    }
    
    private void Start() {
        float elapsedTime = GameManager.Instance.GetElapsedTime();
        
        float noise = Mathf.PerlinNoise(noiseOffset, elapsedTime * noiseSpeed) * 2f - 1f;

        currentSpeed = Mathf.Min(maximumSpeed, initialSpeed + (elapsedTime * growthRate) + (noise * fluctationMagnitude));

        Debug.Log($"[TileScript] Time: {elapsedTime:F2}s | Tile Speed: {currentSpeed:F2}");
    }
    
    private void Update()
    {
        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
        if (transform.position.y < -8)
        {
            DisableTile();
        }
    }


    private void OnEnable() {
        TypingManagerScript manager = FindFirstObjectByType<TypingManagerScript>();

        if (manager != null) {
            manager.RegisterTile(this);
        } else {
            Debug.LogError("Typing Manager not found in the scene");
        }
    }

    private void OnDisable() {
        TypingManagerScript manager = FindFirstObjectByType<TypingManagerScript>();

        if (manager != null) {
            manager.UnregisterTile(this);
        }
    }

    public void DisableTile()
    {
        tileWord = "";
        if (textMesh != null)
        {
            textMesh.text = "";
        }

        gameObject.SetActive(false);
        FindFirstObjectByType<TypingManagerScript>()?.UnregisterTile(this);
        TilePool.Instance.ReturnTile(this);
    }
}
