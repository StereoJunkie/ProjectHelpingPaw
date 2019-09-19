using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTurn : MonoBehaviour
{
    [SerializeField] private DayAndNightCycle dayNight;
    private Image image;

    private float timePassed;

    [SerializeField] private float timePassedSeconds;

    [SerializeField] private float lastSecond;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNight.timeActive)
        {
            timePassed += Time.deltaTime;
            timePassedSeconds += Time.deltaTime;
            if (timePassed >= dayNight.minutesInADay*60f)
            {
                timePassed = 0f;
                image.fillAmount = 1;
            }

            if((int)timePassedSeconds> (int)lastSecond)
            {
                lastSecond = timePassedSeconds;
                image.fillAmount -= 1 / (dayNight.minutesInADay * 60f);
            }
        }
    }
}
