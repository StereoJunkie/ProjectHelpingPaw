using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDog : MonoBehaviour
{
    [Range(1f, 10f)] [SerializeField] private int spawnRatePerDay;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject prefab_dog;
    [SerializeField] private List<float> timestamps;

    private BoxCollider colliderBox;
    private DayAndNightCycle dayCycle;
    private DailyStatusEffectCheck dailyCheck;
    private float timesToCheckPerDay;
    [SerializeField] private List<float> timeStampsToCheck;
    private int lastMinute;
    private List<bool> spawnedPerTimeStamp;
    private int lastDay;
    private RoomManager roomManager;
    [SerializeField] private GameObject outside;

    private bool initialSpawn = false;
    
    void Start()
    {
        dayCycle = gameManager.GetComponent<DayAndNightCycle>();
        dailyCheck = gameManager.GetComponent<DailyStatusEffectCheck>();
        colliderBox = GetComponent<BoxCollider>();
        timeStampsToCheck = new List<float>();
        spawnedPerTimeStamp = new List<bool>();
        lastDay = 0;
        roomManager = gameManager.GetComponent<RoomManager>();
    
        if (dailyCheck != null)
        {
            for (int i = 1; i <= spawnRatePerDay; i++)
            {
                float input = dayCycle.minutesInADay / spawnRatePerDay * i;
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
        if (!initialSpawn)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(colliderBox.transform.position.x-colliderBox.size.x*0.5f,colliderBox.transform.position.x-colliderBox.size.x*0.5f+colliderBox.size.x),Random.Range(colliderBox.transform.position.y-colliderBox.size.y*0.5f,colliderBox.transform.position.y-colliderBox.size.y*0.5f+colliderBox.size.y),Random.Range(colliderBox.transform.position.z-colliderBox.size.z*0.5f,colliderBox.transform.position.z-colliderBox.size.z*0.5f+colliderBox.size.z));
            GameObject spawnedDog = Instantiate(prefab_dog, spawnLocation, prefab_dog.transform.rotation);
            roomManager.dogs.Add(spawnedDog);
            spawnedDog.transform.parent = outside.transform;
            initialSpawn = true;
        }
        if (roomManager.developerMode)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Vector3 spawnLocation = new Vector3(Random.Range(colliderBox.transform.position.x-colliderBox.size.x*0.5f,colliderBox.transform.position.x-colliderBox.size.x*0.5f+colliderBox.size.x),Random.Range(colliderBox.transform.position.y-colliderBox.size.y*0.5f,colliderBox.transform.position.y-colliderBox.size.y*0.5f+colliderBox.size.y),Random.Range(colliderBox.transform.position.z-colliderBox.size.z*0.5f,colliderBox.transform.position.z-colliderBox.size.z*0.5f+colliderBox.size.z));
                GameObject spawnedDog = Instantiate(prefab_dog, spawnLocation, prefab_dog.transform.rotation);
                spawnedDog.transform.parent = outside.transform;
                roomManager.dogs.Add(spawnedDog);
            }
        }
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
                    Vector3 spawnLocation = new Vector3(Random.Range(colliderBox.transform.position.x-colliderBox.size.x*0.5f,colliderBox.transform.position.x-colliderBox.size.x*0.5f+colliderBox.size.x),Random.Range(colliderBox.transform.position.y-colliderBox.size.y*0.5f,colliderBox.transform.position.y-colliderBox.size.y*0.5f+colliderBox.size.y),Random.Range(colliderBox.transform.position.z-colliderBox.size.z*0.5f,colliderBox.transform.position.z-colliderBox.size.z*0.5f+colliderBox.size.z));
                    GameObject spawnedDog = Instantiate(prefab_dog, spawnLocation, prefab_dog.transform.rotation);
                    spawnedDog.transform.parent = outside.transform;
                    roomManager.dogs.Add(spawnedDog);
                }
            }
        }
    }
}
