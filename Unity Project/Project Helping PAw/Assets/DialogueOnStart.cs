using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DialogueOnStart : MonoBehaviour
{
    [SerializeField] public List<DialogueTrigger> startingText;
    private bool played;
    
    public bool ClickedDog;
    public bool ZoomIntoRoom;
    public bool OpenedActivityPanel;
    public bool EndTutorial;

    private void Start()
    {
        played = false;

    }

    private void Update()
    {
        if (!played && FindObjectOfType<DialogueManager>())
        {
            if(startingText != null)
                startingText[0].TriggerDialogue();
            else
            {
                Debug.Log("Can't find dialogue");
            }

            played = true;
        }

        if (ClickedDog)
        {
            startingText[1].TriggerDialogue();
            ClickedDog = false;
        }

        if (ZoomIntoRoom)
        {
            startingText[2].TriggerDialogue();
            ZoomIntoRoom = false;
        }

        if (OpenedActivityPanel)
        {
            startingText[3].TriggerDialogue();
            OpenedActivityPanel = false;
        }

        if (EndTutorial)
        {
            startingText[4].TriggerDialogue();
            EndTutorial = false;
        }
    }
}
