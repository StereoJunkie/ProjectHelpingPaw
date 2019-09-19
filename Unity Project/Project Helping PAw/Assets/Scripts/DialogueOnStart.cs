using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueOnStart : MonoBehaviour
{
    [SerializeField] public List<DialogueTrigger> startingText;
    private bool played;
    
    public bool ClickedDog;
    public bool ZoomIntoRoom;
    public bool OpenedActivityPanel;
    public bool EndTutorial;
    [SerializeField] public bool hasEnded;
    [SerializeField] private AudioSource soundtrack;

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

        if (SceneManager.GetActiveScene().name == "level2")
        {
            OpenedActivityPanel = true;
            Debug.Log("Testttt");
        }

        if (SceneManager.GetActiveScene().name == "level1")
        {
            if (ClickedDog && played)
            {
                startingText[1].TriggerDialogue();
                ClickedDog = false;
            }

            if (ZoomIntoRoom && !ClickedDog)
            {
                startingText[2].TriggerDialogue();
                ZoomIntoRoom = false;
            }

            if (OpenedActivityPanel && !ZoomIntoRoom)
            {
                startingText[3].TriggerDialogue();
                OpenedActivityPanel = false;
                hasEnded = true;
            }

            if (EndTutorial && !OpenedActivityPanel)
            {
                hasEnded = false;
                startingText[4].TriggerDialogue();
                EndTutorial = false;
                soundtrack.Play();
            }
        }
    }
}
