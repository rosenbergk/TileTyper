// TextShakeEffect.cs
using System.Collections;
using UnityEngine;

public class TextShakeEffect : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 20f;

    private Vector3 originalPosition;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError(
                "TextShakeEffect: No RectTransform found! Make sure this is a UI element."
            );
        }
        originalPosition = rectTransform.anchoredPosition;
    }

    public void TriggerShake()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

            rectTransform.anchoredPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
