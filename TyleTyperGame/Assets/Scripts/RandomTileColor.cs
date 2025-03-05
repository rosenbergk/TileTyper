// RandomTileColor.cs
using UnityEngine;

public class RandomTileColor : MonoBehaviour
{
    private void OnEnable()
    {
        Color randomColor;
        do
        {
            randomColor = Random.ColorHSV();
        } while (randomColor.Equals(Color.white));

        Debug.Log("Tile OnEnable called. Random Color: " + randomColor);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = randomColor;
        }
    }
}
