using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class createRooms : MonoBehaviour
{
    private int roomAmount;
    private GameObject room_Prefab;
    private BoxCollider roomCollider;
    private RoomManager roomManager;

    private void Start()
    {
        roomManager = GetComponent<RoomManager>();
        roomAmount = roomManager.roomAmount;
        room_Prefab = roomManager.prefab_Room;
        roomCollider = room_Prefab.GetComponent<BoxCollider>();
        generateRooms();
    }
    
    private void generateRooms()
    {
        int amountRoomSpawned = 0;
        Vector3 bottomLeftest = new Vector3(0,0,0);
        for (int j = 0; j < roomAmount; j++)
        {
            for (int i = 0; i < j+1; ++i)
            {
                if (j == 0)
                {
                    Instantiate(room_Prefab, bottomLeftest, Quaternion.identity);
                    amountRoomSpawned++;
                }
                else
                {
                    if (amountRoomSpawned < roomAmount)
                    {
                        Vector3 spawnLocation =
                            bottomLeftest + new Vector3(-roomCollider.size.x * i, 0, -roomCollider.size.z*i);
                        Instantiate(room_Prefab, spawnLocation, Quaternion.identity);
                    }
                    amountRoomSpawned++;
                }
            }
            bottomLeftest = bottomLeftest + new Vector3(roomCollider.size.x, roomCollider.size.y, 0);
        }
    }
}
