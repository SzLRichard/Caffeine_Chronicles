using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float timePassed;
    bool isRunning;
    void Start()
    {
        isRunning = true;
    }
    void Update()
    {
        if (isRunning) {
            timePassed += Time.deltaTime;
            DisplayTime(timePassed);
        }
        
    }
    void DisplayTime(float time) {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float miliseconds = (time * 1000) % 1000;

        timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds,miliseconds);
    }
    public float getTimeElapsed() {
        return timePassed;
    }
    public void setTimerActive(bool state) {
        isRunning = state;
    }
}
