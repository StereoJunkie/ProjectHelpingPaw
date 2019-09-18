﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = System.Random;

public class createRooms : MonoBehaviour
{
    private int roomAmount;
    private List<GameObject> room_Prefab;
    private GameObject outside_Prefab;
    private BoxCollider roomCollider;
    private RoomManager roomManager;
    private GameObject spawnedRoom;

    private void Start()
    {
        roomManager = GetComponent<RoomManager>();
        roomAmount = roomManager.roomAmount;
        outside_Prefab = roomManager.outside_Prefab;
        room_Prefab = roomManager.prefab_Rooms;
        roomCollider = room_Prefab[0].GetComponent<BoxCollider>();
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
                    int random = UnityEngine.Random.Range(0, 3);
                    spawnedRoom = Instantiate(room_Prefab[random], bottomLeftest, room_Prefab[random].transform.rotation);
                    spawnedRoom.tag = "Room";
                    roomManager.rooms.Add(spawnedRoom);
                    amountRoomSpawned++;
                }
                else
                {
                    if (amountRoomSpawned < roomAmount)
                    {
                        int random = UnityEngine.Random.Range(0, 3);
                        Vector3 spawnLocation =
                            bottomLeftest + new Vector3(-roomCollider.size.x * i, 0, -roomCollider.size.z*i);
                        spawnedRoom = Instantiate(room_Prefab[random], spawnLocation, room_Prefab[random].transform.rotation);
                        spawnedRoom.tag = "Room";
                        roomManager.rooms.Add(spawnedRoom);
                    }
                    amountRoomSpawned++;
                }
            }
            bottomLeftest = bottomLeftest + new Vector3(roomCollider.size.x, roomCollider.size.y, 0);
        }
    }
}
