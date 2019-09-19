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
    [SerializeField] private DialogueTrigger dialogueLose;

    void Update()
    {
        if (roomManager.DogsAdopted > amountAdoptedToWin)
        {
            dialogueWin.TriggerDialogue();
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2)
                {
                    SceneManager.LoadScene(2);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(2);
            }
        }
        else if (roomManager.DogsKilled > amountAdoptedToLose)
        {
            dialogueLose.TriggerDialogue();
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2)
                {
                    SceneManager.LoadScene(1);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(1);
            }
        }

        
    }
}
