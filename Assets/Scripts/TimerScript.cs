using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private float timeElapsed;
    private bool timerIsRunning;

    void Start()
    {
        timerIsRunning = true;
        startTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed = Time.timeSinceLevelLoad - startTime;
            string minutes = Mathf.Floor(timeElapsed / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public float StopTimer()
    {
        timerIsRunning = false;
        return timeElapsed;
    }
}
