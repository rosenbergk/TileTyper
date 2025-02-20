using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TypingManagerScript : MonoBehaviour {
    public TextMeshPro playerInputText;
    public TextShakeEffect textShakeEffect;

    private string currentInput = "";
    private List<TileScript> activeTiles = new List<TileScript>();
    private Coroutine blinkingCoroutine;
    private bool isCursorVisible = true;
    private bool hasStartedBlinking = false;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += StartCursorBlinking;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= StartCursorBlinking; // ✅ Unsubscribe to prevent errors
    }

    private void StartCursorBlinking()
    {
        if (!hasStartedBlinking)
        {
            hasStartedBlinking = true;
            blinkingCoroutine = StartCoroutine(BlinkCursor()); // ✅ Start blinking when game starts
        }
    }


    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted()) 
        {
            if (playerInputText != null)
            {
                playerInputText.text = ""; // Hide cursor during countdown
            }
            return;
        }
        HandleTypingInput();
    }

    private void HandleTypingInput() {
        foreach (char c in Input.inputString) {
            if (c == '\b' && currentInput.Length > 0) {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            } else if (c == '\r' || c == '\n') {
                CheckForMatchingTile();
            } else if (!char.IsControl(c)) {
                currentInput += c;
                AudioManager.Instance.PlayTypingSound();
            }

            playerInputText.text = currentInput;
        }
        UpdateCursorDisplay();
    }
    private void UpdateCursorDisplay()
    {
        if (playerInputText != null)
        {
            string cursorColor = isCursorVisible ? "<alpha=#FF>" : "<alpha=#00>";
            playerInputText.text = currentInput + cursorColor + "_</color>";        
        }
    }

    private void CheckForMatchingTile() {
        bool wordFound = false;
        
        for (int i = 0; i < activeTiles.Count; ++i) {
            if (activeTiles[i].tileWord.Equals(currentInput, System.StringComparison.OrdinalIgnoreCase)) {
                GameManager.Instance.AddScore();
                AudioManager.Instance.PlayCorrectWordSound();
                activeTiles[i].DisableTile();
                currentInput = "";

                playerInputText.text = currentInput;

                wordFound = true;

                break;
            }
        }

        if (!wordFound) {
            if (textShakeEffect != null) {
                textShakeEffect.TriggerShake();
            }
        }
    }

    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            isCursorVisible = !isCursorVisible;
            UpdateCursorDisplay();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void RegisterTile(TileScript tile) {
        activeTiles.Add(tile);
    }

    public void UnregisterTile(TileScript tile) {
        activeTiles.Remove(tile);
    }
}