using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwipeUp : MonoBehaviour
{
    [SerializeField] private SelectedManager selectedManager;
    
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
            wentDown = false;
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
        else if(!wentDown)
        {
            GoDown();
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
    }
    
    private void GoUp()
    {
        animator.SetTrigger("GoUp 0");
        
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
        wentDown = true;
        animator.SetTrigger("GoDown 0");
    }

    private void MoveLogic()
    {
        if ((endPosition - startPosition).magnitude > 25)
        {
            print((endPosition - startPosition).magnitude);
            print((endPosition - startPosition));
            startToEndVector = (endPosition - startPosition).normalized;
            angle = Math.Abs(Vector2.Dot(startToEndVector, Vector2.down));
            Debug.Log(angle);
            Debug.Log(startToEndVector);
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
