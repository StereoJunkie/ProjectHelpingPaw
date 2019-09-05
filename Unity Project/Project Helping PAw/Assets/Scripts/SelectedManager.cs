using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedManager : MonoBehaviour
{
    [Range(0.01f, 1f)] [SerializeField] private float maxClickHold;
    [Range(0.1f, 5f)] [SerializeField] private float cameraLerpSpeed;
    [SerializeField] private GameObject highlightRoom;
    [Range(1f,20f)][SerializeField] private int cameraOffset;
    [Range(1f, 5f)] [SerializeField] private float zoomAmount;
    [Range(1f, 10f)] [SerializeField] private float zoomSpeed;
    
    private Vector3 highlightedRoomCenter;
    private float tempTime;
    private RoomManager roomManager;
    private Camera mainCamera;
    private BoxCollider prefabCollider;
    private Vector3 centerOffset;
    private float originalZoom;
    private Vector2 tempVector;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        mainCamera = Camera.main;
        centerOffset = new Vector3(prefabCollider.size.x * 0.5f,
            prefabCollider.size.y * 0.5f, prefabCollider.size.z * 0.5f);
        originalZoom = mainCamera.orthographicSize;
    }

    private void Update()
    {
        Select();
        if (highlightRoom != null && mainCamera != null)
        {
            highlightedRoomCenter = highlightRoom.transform.position + centerOffset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                highlightedRoomCenter + new Vector3(-cameraOffset,cameraOffset*1.5f,cameraOffset), cameraLerpSpeed * Time.deltaTime);
            tempVector = Vector2.Lerp(new Vector2(mainCamera.orthographicSize,mainCamera.orthographicSize), new Vector2(zoomAmount,zoomAmount), zoomSpeed * Time.deltaTime);
            mainCamera.orthographicSize = tempVector.x;
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
                tempTime = 0f;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                tempTime = 0f;
            }
        }
    }
}
