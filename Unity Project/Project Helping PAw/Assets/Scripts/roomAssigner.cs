using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class roomAssigner : MonoBehaviour
{
    private GameObject gameManager;
    private RoomManager roomManager;
    private List<GameObject> listOfRooms;
    private List<GameObject> dogs;
    private SelectedManager selectionManager;
    public GameObject highlightDog;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            roomManager = gameManager.GetComponent<RoomManager>();
        }
        else Debug.LogError("No gameManager found in " + name);

        if (roomManager != null)
        { 
            listOfRooms = new List<GameObject>();

        }
        else Debug.LogError("No gameManager found in " + name);

        selectionManager = FindObjectOfType<SelectedManager>();
        
    }

    private void Update()
    {
        if (listOfRooms.Count == 0)
        {
            listOfRooms = roomManager.rooms;
        }
        
        if (highlightDog != null)
        {
            foreach (GameObject room in listOfRooms)
            {
                Room roomInfo = room.GetComponent<Room>();
                BoxCollider roomCollider = room.GetComponent<BoxCollider>();
                if (roomInfo.dog == null)
                {
                    roomInfo.dog = highlightDog;
                    Dog dog = highlightDog.GetComponent<Dog>();
                    if (dog != null)
                    {
                        dog.room = room;
                        dog.Homeless = false;
                        dog.Sheltered = true; 
                    }
                    
                    highlightDog.transform.position = room.transform.position + new Vector3(roomCollider.size.x * 0.5f,roomCollider.size.y * 0.5f,-roomCollider.size.z * 0.5f) ;
                    highlightDog.transform.parent = room.transform;
                    highlightDog = null;
                    break;
                }
            }
        }
    }
}
