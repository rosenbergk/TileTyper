// TileScript.cs
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public String tileWord;
    public static float currentSpeed;
    public static float initialSpeed = 0.5f;

    [SerializeField]
    private float maximumSpeed = 5f;

    [SerializeField]
    private float growthRate = 0.02f;

    [SerializeField]
    private float fluctationMagnitude = 0.4f;

    [SerializeField]
    private float noiseSpeed = 0.04f;

    private TextMeshPro textMesh;
    private float noiseOffset;
    private Vector3 originalScale;

    public void SetTileWord(String word)
    {
        tileWord = word;
        if (textMesh != null)
        {
            textMesh.text = word;
        }
    }

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        noiseOffset = UnityEngine.Random.Range(0f, 100f);
        originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * currentSpeed * Time.deltaTime, Space.World);
        if (transform.position.y < -8)
        {
            DisableTile();
        }
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;

        TypingManagerScript manager = FindFirstObjectByType<TypingManagerScript>();
        if (manager != null)
        {
            manager.RegisterTile(this);
        }
        else
        {
            Debug.LogError("Typing Manager not found in the scene");
        }

        float elapsedTime = GameManager.Instance.GetElapsedTime();

        float noise = Mathf.PerlinNoise(noiseOffset, elapsedTime * noiseSpeed) * 2f - 1f;

        currentSpeed = Mathf.Min(
            maximumSpeed,
            initialSpeed + (elapsedTime * growthRate) + (noise * fluctationMagnitude)
        );

        Debug.Log($"[TileScript] Time: {elapsedTime:F2}s | Tile Speed: {currentSpeed:F2}");
    }

    private void OnDisable()
    {
        TypingManagerScript manager = FindFirstObjectByType<TypingManagerScript>();

        if (manager != null)
        {
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

    public IEnumerator ZoomOutAndDisable(float duration)
    {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsed / duration);
            yield return null;
        }

        transform.localScale = Vector3.zero;
        DisableTile();
    }
}
