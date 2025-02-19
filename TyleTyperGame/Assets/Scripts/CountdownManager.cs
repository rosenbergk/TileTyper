using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public TextMeshPro countdownText;
    private float countdownTime = 5f;

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private System.Collections.IEnumerator CountdownRoutine()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString("0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        GameManager.Instance.StartGame();
    }
}