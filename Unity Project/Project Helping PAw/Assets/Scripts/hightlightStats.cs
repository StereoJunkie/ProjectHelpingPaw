using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightlightStats : MonoBehaviour
{
    [SerializeField] private bool showNeeds = false;
    [SerializeField] private SelectedManager highlightedDog;
    public Dog dogStats;
    private GameObject dog;

    void Start()
    {
        showNeeds = false;
    }

    void Update()
    {
        if (highlightedDog != null && highlightedDog.highlightRoom != null && highlightedDog.highlightRoom.GetComponent<Room>().dog != null)
        {
            dog = highlightedDog.highlightRoom.GetComponent<Room>().dog;
            dogStats = dog.GetComponentInChildren<Dog>();
            if(dogStats!=null)
                showNeeds = true;
        }
        else
        {
            showNeeds = false;
        }


        if (showNeeds)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}