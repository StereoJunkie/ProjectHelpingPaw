using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDog : MonoBehaviour
{
    [Range(1f, 10f)] [SerializeField] private int spawnRatePerDay;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject prefab_dog;
    [SerializeField] private List<int> timestamps;

    private BoxCollider colliderBox;
    private DayAndNightCycle dayCycle;
    private DailyStatusEffectCheck dailyCheck;
    private int timesToCheckPerDay;
    private List<int> timeStampsToCheck;
    private int lastMinute;
    private List<bool> spawnedPerTimeStamp;
    private int lastDay;
    
    void Start()
    {
        dayCycle = gameManager.GetComponent<DayAndNightCycle>();
        dailyCheck = gameManager.GetComponent<DailyStatusEffectCheck>();
        colliderBox = GetComponent<BoxCollider>();
        timeStampsToCheck = new List<int>();
        spawnedPerTimeStamp = new List<bool>();
        lastDay = 0;
        

        if (dailyCheck != null)
        {
            for (int i = 1; i <= spawnRatePerDay; i++)
            {
                int input = dayCycle.minutesInADay / spawnRatePerDay * i;
                timeStampsToCheck.Add(input);
            }
            for (int i = 0; i < timeStampsToCheck.Count; i++)
            {
                spawnedPerTimeStamp.Add(false);
            }
        }
        

        timestamps = timeStampsToCheck;
    } 

    void Update()
    {
        if (dayCycle.timeActive)
        {
            if ((int)dayCycle.DaysSinceStart > lastDay)
            {
                for (int i = 0; i < spawnedPerTimeStamp.Count; i++)
                {
                    spawnedPerTimeStamp[i] = false;
                }

                lastDay = (int) dayCycle.DaysSinceStart;
            }
            for (int i = 0; i < timeStampsToCheck.Count; i++)
            {
                if (timeStampsToCheck[i] == dayCycle.timePassedMinPerDay && !spawnedPerTimeStamp[i])
                {
                    spawnedPerTimeStamp[i] = true;
                    Vector3 spawnLocation = new Vector3(Random.Range(colliderBox.transform.position.x,colliderBox.transform.position.x+colliderBox.size.x),Random.Range(colliderBox.transform.position.y,colliderBox.transform.position.y+colliderBox.size.y),Random.Range(colliderBox.transform.position.z,colliderBox.transform.position.z+colliderBox.size.z));
                    Instantiate(prefab_dog, spawnLocation, Quaternion.identity);
                }
            }
        }
    }
}
