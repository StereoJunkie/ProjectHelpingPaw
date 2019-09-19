using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogActivities : MonoBehaviour
{
    private GameObject gameManager;
    private SelectedManager selection;
    private GameObject selector;
    private AnimalStatManager StatManager;
    private Volunteers volunteers;

    private GameObject roomObject;
    private Room highlightedRoom;
    [SerializeField] private DialogueOnStart dialogue;
    [SerializeField] private Dog dog;
    private bool feed = false;
    private bool walk = false;
    private bool wash = false;
    private bool groom = false;
    private bool medicalCheckup = false;
    private Text volunteerCounter;

    private bool activityTimer = false;
    private float maxTimerTime;
    private float timePassed = 0f;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        selector = GameObject.Find("SelectedManager");
        selection = selector.GetComponent<SelectedManager>();
        volunteerCounter = GetComponentInChildren<Text>();
        if (gameManager != null)
        {
            StatManager = gameManager.GetComponent<AnimalStatManager>();
            volunteers = gameManager.GetComponent<Volunteers>();
        }
        else
        {
            Debug.LogError(name + " Could not find the gameManager. does gameManager exist?");
        }

        if (StatManager != null)
        {
            maxTimerTime = StatManager.ActivityTimer;
        }
        else
        {
            Debug.LogError(name + " gameManager probably doesn't have a statManager");
        }

        dog = GetComponent<Dog>();
    }

    private void Update()
    {
        if (timePassed > maxTimerTime)
        {
            timePassed = 0f;
            activityTimer = false;
            volunteers.VolunteersInUse -= 1;
        }
        if(volunteers.VolunteersInUse > 0)
            timePassed += Time.deltaTime;

        roomObject = selection.highlightRoom;
        if (roomObject != null)
        {
            highlightedRoom = roomObject.GetComponent<Room>();
            if (highlightedRoom != null)
            {
                if (highlightedRoom.dog != null)
                    dog = highlightedRoom.dog.GetComponentInChildren<Dog>();
            }
            else
            {
                Debug.Log("no highlighted room script ");
            }
        }

        if (volunteerCounter != null && volunteers != null)
        {
            volunteerCounter.text = volunteers.VolunteersInUse + "/" + volunteers.AmountVolunteers;
        }
    }

    public void Feed()
    {
        if (dog != null && !dialogue.OpenedActivityPanel)
        {
            if (volunteers.VolunteersInUse < volunteers.AmountVolunteers)
            {
                dog.Nutrition += 25f;
                dog.poopTimer = true;
                activityTimer = true;
                volunteers.VolunteersInUse += 1;
            }
        }
    }

    public void Walk()
    {
        if (dog != null && !dialogue.hasEnded)
        {
            if (volunteers.VolunteersInUse < volunteers.AmountVolunteers)
            {
                dog.poopTimer = false;
                dog.poopTimePassed = 0f;
                dog.dirtyChance += 10f;
                dog.ExtraDrainageHealth += 4f;
                dog.Socialization += 25f;
                activityTimer = true;
                volunteers.VolunteersInUse += 1;
            }
        }
    }

    public void Wash()
    {
        if (dog != null && !dialogue.hasEnded)
        {
            if (volunteers.VolunteersInUse < volunteers.AmountVolunteers)
            {
                dog.Hygiene += 25f;
                foreach (StatusEffect effect in dog.activeEffects)
                {
                    if (effect.name == "Dirty")
                    {
                        effect.ActivateEffect = false;
                        break;
                    }
                }

                activityTimer = true;
                volunteers.VolunteersInUse += 1;
            }
        }
    }

    public void Groom()
    {
        if (dog != null && !dialogue.hasEnded)
        {
            if (volunteers.VolunteersInUse < volunteers.AmountVolunteers)
            {
                foreach (StatusEffect effect in dog.activeEffects)
                {
                    if (effect.name == "Ungroomed")
                    {
                        effect.ActivateEffect = false;
                        break;
                    }
                }

                activityTimer = true;
                volunteers.VolunteersInUse += 1;
            }
        }
    }

    public void MedicalCheckup()
    {
        if (dog != null && !dialogue.hasEnded)
        {
            if (volunteers.VolunteersInUse < volunteers.AmountVolunteers)
            {
                foreach (StatusEffect effect in dog.activeEffects)
                {
                    if (effect.name == "Sick" || effect.name == "Wounded")
                    {
                        effect.ActivateEffect = false;
                    }
                }

                activityTimer = true;
                volunteers.VolunteersInUse += 1;
            }
        }
    }

    public void Euthanize()
    {
        if (dog != null && !dialogue.hasEnded)
        {
            dog.Health = 0f;
            timePassed = 0f;
            activityTimer = false;
            volunteers.VolunteersInUse -= 1;
        }
    }
}