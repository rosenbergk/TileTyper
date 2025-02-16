using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class TypingManagerScript : MonoBehaviour {
    public TextMeshPro playerInputText;
    private string currentInput = "";
    private List<TileScript> activeTiles = new List<TileScript>();

    private void Update()
    {
        HandleTypingInput();
    }

    private void HandleTypingInput() {
        foreach (char c in Input.inputString) {
            if (c == '\b' && currentInput.Length > 0) {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            } else if (c == '\r' || c == '\n') {
                CheckForMatchingTile();
                currentInput = "";
            } else if (!char.IsControl(c)) {
                currentInput += c;
            }

            playerInputText.text = currentInput;
        }
    }

    private void CheckForMatchingTile() {
        for (int i = 0; i < activeTiles.Count; ++i) {
            if (activeTiles[i].tileWord.Equals(currentInput, System.StringComparison.OrdinalIgnoreCase)) {
                activeTiles[i].DisableTile();
                currentInput = "";

                playerInputText.text = currentInput;

                break;
            }
        }
    }

    public void RegisterTile(TileScript tile) {
        activeTiles.Add(tile);
    }

    public void UnregisterTile(TileScript tile) {
        activeTiles.Remove(tile);
    }
}