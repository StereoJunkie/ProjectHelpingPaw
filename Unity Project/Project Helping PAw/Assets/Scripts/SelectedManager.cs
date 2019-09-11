using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedManager : MonoBehaviour
{
    [Range(0.01f, 1f)] [SerializeField] private float maxClickHold;
    [Range(0.1f, 5f)] [SerializeField] private float cameraLerpSpeed;
    [SerializeField] private GameObject highlightRoom;
    [Range(-10f,20f)][SerializeField] private int cameraOffset;
    [Range(1f, 5f)] [SerializeField] private float zoomAmount;
    [Range(1f, 10f)] [SerializeField] private float zoomSpeed;
    [Range(1f, 10f)] [SerializeField] private float roomMoveSpeed;
    
    private Vector3 highlightedRoomCenter;
    private float tempTime;
    private RoomManager roomManager;
    private Camera mainCamera;
    private Vector3 mainCameraOrigin;
    private BoxCollider prefabCollider;
    private Vector3 centerOffset;
    private float originalZoom;
    private Vector2 tempVector;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        mainCamera = Camera.main;
        mainCameraOrigin = mainCamera.transform.position;
        prefabCollider = roomManager.prefab_Room.GetComponent<BoxCollider>();
        if (prefabCollider != null)
        {
            centerOffset = new Vector3(prefabCollider.size.x * 0.5f,
                prefabCollider.size.y * 0.5f, -prefabCollider.size.z * 0.5f);
        }

        originalZoom = mainCamera.orthographicSize;
    }

    private void Update()
    {
        Select();
        if (highlightRoom != null && mainCamera != null)
        {
            highlightedRoomCenter = highlightRoom.transform.position + centerOffset;
            Vector3 newCameraPosition = highlightedRoomCenter + new Vector3(-cameraOffset, cameraOffset, cameraOffset);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                    newCameraPosition,
                    cameraLerpSpeed * Time.deltaTime);

                tempVector = Vector2.Lerp(new Vector2(mainCamera.orthographicSize,mainCamera.orthographicSize), new Vector2(zoomAmount,zoomAmount), zoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = tempVector.x;
            foreach (GameObject room in roomManager.rooms)
            {
                if (room != highlightRoom)
                {
                    Room tempRoom = room.GetComponent<Room>();
                    room.transform.position =
                        Vector3.Lerp(room.transform.position, new Vector3(tempRoom.spawnPosition.x, tempRoom.spawnPosition.y+20, tempRoom.spawnPosition.z),roomMoveSpeed * Time.deltaTime);
                    
                }
            }
        }
        else if (highlightRoom == null)
        {
            if (mainCamera.transform.position == mainCameraOrigin)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                    mainCameraOrigin, cameraLerpSpeed * Time.deltaTime);
            }

            tempVector = Vector2.Lerp(new Vector2(mainCamera.orthographicSize,mainCamera.orthographicSize), new Vector2(originalZoom,originalZoom), zoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = tempVector.x;
            foreach (GameObject room in roomManager.rooms)
            {
                if (room != highlightRoom)
                {
                    Room tempRoom = room.GetComponent<Room>();
                    room.transform.position =
                        Vector3.Lerp(room.transform.position, new Vector3(tempRoom.spawnPosition.x, tempRoom.spawnPosition.y, tempRoom.spawnPosition.z),roomMoveSpeed * Time.deltaTime);
                    
                }
            }
        }
        
    }
    
    private void Select()
    {
        //developer mode
        if (roomManager.developerMode)
        {
            if (Input.GetMouseButton(0))
            {
                tempTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) && tempTime < maxClickHold)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject selection = hit.transform.gameObject;
                    if (selection != null && selection.CompareTag("Room"))
                    {
                        highlightRoom = selection;
                    }
                    
                }
                else
                {
                    highlightRoom = null;
                }
                tempTime = 0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                tempTime = 0f;
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                tempTime += Time.deltaTime;
            }

            if (touch.phase == TouchPhase.Ended && tempTime < maxClickHold)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject selection = hit.transform.gameObject;
                    if (selection != null && selection.CompareTag("Room"))
                    {
                        highlightRoom = selection;
                    }
                    
                }
                else
                {
                    highlightRoom = null;
                }
                tempTime = 0f;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                tempTime = 0f;
            }
        }
    }
}
