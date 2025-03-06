// CountdownManager.cs
using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    private float countdownTime = 5f;
    private bool countdownStarted = false;

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

    private void OnEnable()
    {
        countdownTime = 5f;
        countdownStarted = false;
        countdownText.gameObject.SetActive(true);
        StartCountdown();
    }
}
