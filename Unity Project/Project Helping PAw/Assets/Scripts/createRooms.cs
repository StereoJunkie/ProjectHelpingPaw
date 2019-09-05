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
    private GameObject spawnedRoom;

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
                    spawnedRoom = Instantiate(room_Prefab, bottomLeftest, Quaternion.identity);
                    spawnedRoom.tag = "Room";
                    amountRoomSpawned++;
                }
                else
                {
                    if (amountRoomSpawned < roomAmount)
                    {
                        Vector3 spawnLocation =
                            bottomLeftest + new Vector3(-roomCollider.size.x * i, 0, -roomCollider.size.z*i);
                        spawnedRoom = Instantiate(room_Prefab, spawnLocation, Quaternion.identity);
                        spawnedRoom.tag = "Room";
                    }
                    amountRoomSpawned++;
                }
            }
            bottomLeftest = bottomLeftest + new Vector3(roomCollider.size.x, roomCollider.size.y, 0);
        }
    }
}
