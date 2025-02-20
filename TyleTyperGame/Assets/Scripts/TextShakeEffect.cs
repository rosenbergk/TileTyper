using UnityEngine;
using TMPro;
using System.Collections;

public class TextShakeEffect : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Vector3 originalPosition;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        originalPosition = transform.localPosition;
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

            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}