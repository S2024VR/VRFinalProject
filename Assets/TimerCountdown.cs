using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;  // Required for UnityEvent

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    private Text countdownDisplay;

    public UnityEvent OnCountdownFinished;  // Event that gets triggered when countdown finishes

    private void Start()
    {
        countdownDisplay = GetComponent<Text>();
        countdownTime = 120;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        OnCountdownFinished.Invoke();  // Trigger the event when countdown finishes
    }
}
