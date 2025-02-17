// TileScript.cs
using System;
using UnityEngine;
using TMPro;

public class TileScript : MonoBehaviour
{
    public String tileWord;

    [SerializeField] private float initialSpeed = 0.5f;
    [SerializeField] private float maximumSpeed = 5f;
    [SerializeField] private float spawnIntervalAcceleration = 0.05f;
    private float currentSpeed;
    private TextMeshPro textMesh;

    public void SetTileWord(String word) {
        tileWord = word;
        if (textMesh != null) {
            textMesh.text = word;
            Debug.Log("Tile created with word " + word);
        } else {
            Debug.Log("Text mesh is null");
        }
    }

    private void Awake() {
        textMesh = GetComponentInChildren<TextMeshPro>();
    }
    
    private void Start() {
        float elapsedTime = GameManager.Instance.GetElapsedTime();
        currentSpeed = Mathf.Min(maximumSpeed, initialSpeed + spawnIntervalAcceleration * elapsedTime);
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
        } else {
            Debug.LogError("Typing Manager not found in the scene");
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
