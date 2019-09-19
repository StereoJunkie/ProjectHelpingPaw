using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwipeUp : MonoBehaviour
{
    [SerializeField] private SelectedManager selectedManager;
    [SerializeField] private RoomManager roomManager;
    
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 startToEndVector;
    private float angle;
    private Animator animator;
    private bool wentDown;
    private bool firstSwipe;
    private DialogueOnStart dialogue;
    private bool swipeOut;

    void Start()
    {
        dialogue = FindObjectOfType<DialogueOnStart>();
        startPosition = new Vector2();
        endPosition = new Vector2();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (selectedManager.highlightRoom != null)
        {
            animator.SetBool("NoHighlight", false);
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPosition = Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    endPosition = Input.GetTouch(0).position;
                    MoveLogic();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPosition = Input.mousePosition;
                MoveLogic();
            }
        }
        else
        {
            animator.SetBool("NoHighlight", true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
    }
    
    private void GoUp()
    {
        animator.ResetTrigger("GoUp 0");
        animator.ResetTrigger("GoDown 0");
        animator.SetTrigger("GoUp 0");
        roomManager.panAble = false;
        
    }

    private void tutorialScreen()
    {
        if (!firstSwipe)
        {
            dialogue.OpenedActivityPanel = true;
            firstSwipe = true;
        }
    }

    private void tutorialEndScreen()
    {
        if (firstSwipe && !swipeOut)
        {
            dialogue.EndTutorial = true;
            swipeOut = true;
        }
    }

    private void GoDown()
    {
        animator.ResetTrigger("GoUp 0");
        animator.ResetTrigger("GoDown 0");
        animator.SetTrigger("GoDown 0");
        roomManager.panAble = true;
    }

    private void MoveLogic()
    {
        if ((endPosition - startPosition).magnitude > 25)
        {
            startToEndVector = (endPosition - startPosition).normalized;
            angle = Math.Abs(Vector2.Dot(startToEndVector, Vector2.down));
            if (angle >= 0.7071f)
            {
                if (startToEndVector.y > 0)
                {
                    GoUp();
                }
                else
                {
                    GoDown();
                }
            }
        }
    }
}
