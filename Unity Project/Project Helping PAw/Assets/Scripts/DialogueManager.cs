using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DayAndNightCycle timeCycle;
    [SerializeField] private Animator animator;
    [SerializeField] private SelectedManager selectionManager;
    [SerializeField] private RoomManager roomManager;

    public Text NameTag;
    public Text DialogueText;
    private Queue<String> Sentences;
    
    

    void Start()
    {
        Sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).deltaTime > 0.2f)
            {
                DisplayNextSentence();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisplayNextSentence();
        }
    }
    

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOn", true);
        timeCycle.timeActive = false;
        selectionManager.Selecting = false;
        roomManager.panAble = false;
        
        
        Sentences.Clear();
        NameTag.text = dialogue.Name;
        foreach (string sentence in dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }

    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOn", false);
        timeCycle.timeActive = true;
        selectionManager.Selecting = true;
        roomManager.panAble = true;
    }
}