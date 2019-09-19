using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [Range(1f,60f)][SerializeField] public int minutesInADay;
    [SerializeField] public float DaysSinceStart;
    [SerializeField] public float timePassedSeconds;
    [SerializeField] public float timePassedMinutes;
    public int timePassedMinPerDay;
    public bool timeActive;
    public int LastMinute;
    public int previousDay;

    private void Start()
    {
        DaysSinceStart = 0;
        timePassedMinutes = 0f;
        timePassedSeconds = 0f;
        timePassedMinPerDay = 0;
        previousDay = 0;

    }

    private void Update()
    {
        if((int) timePassedMinutes > LastMinute)
            LastMinute = (int) timePassedMinutes;
        if (timeActive)
        {
            timePassedSeconds += Time.deltaTime;
            timePassedMinutes = timePassedSeconds / 60f;
        }
        DaysSinceStart = (int)(timePassedMinutes/minutesInADay);
        timePassedMinPerDay = (int)timePassedMinutes;
        if ((int) DaysSinceStart > previousDay)
        {
            previousDay = (int) DaysSinceStart;
            timePassedMinPerDay = 0;
        }
    }
}
