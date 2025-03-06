// RainbowOutline.cs
using UnityEngine;
using UnityEngine.UI;

public class RainbowOutline : MonoBehaviour
{
    [SerializeField]
    private float colorCycleSpeed = 1f;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogError(
                "RainbowOutline script requires an Outline component on the same GameObject."
            );
        }
    }

    private void Update()
    {
        float hue = (Time.time * colorCycleSpeed) % 1f;
        outline.effectColor = Color.HSVToRGB(hue, 1f, 1f);
    }
}
