﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition1 : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private int amountAdoptedToLose;
    [SerializeField] private DialogueTrigger dialogueLose;
    private bool spawnedLoss = false;
    private int clickCount = 0;

    void Update()
    {
        
        if (roomManager.DogsKilled >= amountAdoptedToLose && !spawnedLoss)
        {
            spawnedLoss = true;
            dialogueLose.TriggerDialogue();
            
        }

        if (spawnedLoss)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2)
                {
                    clickCount += 1;
                    if(clickCount >= 3)
                        SceneManager.LoadScene(0);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                clickCount += 1;
                if(clickCount >= 3)
                    SceneManager.LoadScene(0);
            }
        }

        
    }
}
