using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject dog;
    void Start()
    {
        spawnPosition = transform.position;
    }
}
