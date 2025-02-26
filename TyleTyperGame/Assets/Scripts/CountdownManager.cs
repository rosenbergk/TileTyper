// CountdownManager.cs
using TMPro;
using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    private float countdownTime = 5f;
    private bool countdownStarted = false;

    private void OnEnable()
    {
        countdownTime = 5f;
        countdownStarted = false;
        countdownText.gameObject.SetActive(true);
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (!countdownStarted)
        {
            countdownStarted = true;
            StartCoroutine(CountdownRoutine());
        }
    }

    private IEnumerator CountdownRoutine()
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