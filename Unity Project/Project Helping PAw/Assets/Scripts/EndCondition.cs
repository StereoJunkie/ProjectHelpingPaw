using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private int amountAdoptedToWin;
    [SerializeField] private int amountAdoptedToLose;
    [SerializeField] private DialogueTrigger dialogueWin;
    private bool dialogueWinBool;
    private bool dialogueLoseBool;
    [SerializeField] private DialogueTrigger dialogueLose;
    private int clickCount = 0;

    void Update()
    {
        if (roomManager.DogsAdopted >= amountAdoptedToWin && !dialogueWinBool)
        {
            dialogueWinBool = true;
            dialogueWin.TriggerDialogue();
            
        }
        else if (roomManager.DogsKilled >= amountAdoptedToLose && !dialogueLoseBool)
        {
            dialogueLoseBool = true;
            dialogueLose.TriggerDialogue();
            
        }

        if (dialogueWinBool)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2)
                {
                    clickCount += 1;
                    if(clickCount >= 2)
                        SceneManager.LoadScene(2);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                clickCount += 1;
                if(clickCount >= 2)
                    SceneManager.LoadScene(2);
            }
        }

        if (dialogueLoseBool)
        {
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2)
                {
                    clickCount += 1;
                    if(clickCount >= 2)
                        SceneManager.LoadScene(1);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                clickCount += 1;
                if(clickCount >= 2) 
                    SceneManager.LoadScene(1);
            }
        }

        
    }
}
