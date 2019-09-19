using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedManager : MonoBehaviour
{
    [Range(0.01f, 1f)] [SerializeField] private float maxClickHold;
    [Range(0.1f, 5f)] [SerializeField] private float cameraLerpSpeed;
    [SerializeField] public GameObject highlightRoom;
    [Range(-10f, 20f)] [SerializeField] private int cameraOffset;
    [Range(1f, 5f)] [SerializeField] private float zoomAmount;
    [Range(1f, 10f)] [SerializeField] private float zoomSpeed;
    [Range(1f, 10f)] [SerializeField] private float roomMoveSpeed;
    [SerializeField] private GameObject highlightDog;
    [SerializeField] private Animator animator;

    public bool Selecting = true;
    private Vector3 highlightedRoomCenter;
    private float tempTime;
    private RoomManager roomManager;
    private Camera mainCamera;
    private Vector3 mainCameraOrigin;
    private BoxCollider prefabCollider;
    private Vector3 centerOffset;
    private float originalZoom;
    private Vector2 tempVector;
    private bool touchMoved = false;
    private roomAssigner roomAssign;
    private bool positionReseted = false;
    private bool firstClick = false;
    private bool firstClickOnRoom = false;
    private DialogueOnStart dialogue;

    [SerializeField] private GameObject outside;
    private Vector3 outsideOrigin;

    private void Start()
    {
        dialogue = FindObjectOfType<DialogueOnStart>();
        roomAssign = GetComponent<roomAssigner>();
        roomManager = FindObjectOfType<RoomManager>();
        mainCamera = Camera.main;
        outsideOrigin = outside.transform.position;
        mainCameraOrigin = mainCamera.transform.position;
        prefabCollider = roomManager.prefab_Rooms[0].GetComponent<BoxCollider>();
        if (prefabCollider != null)
        {
            centerOffset = new Vector3(prefabCollider.size.x * 0.5f,
                prefabCollider.size.y * 0.5f, -prefabCollider.size.z * 0.5f);
        }

        originalZoom = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if (Selecting)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("SlideUP"))
            {
                Select();
                if (highlightRoom != null && mainCamera != null)
                {
                    roomManager.panAble = false;
                    positionReseted = false;
                    highlightedRoomCenter = highlightRoom.transform.position + centerOffset;
                    if (!firstClickOnRoom && Vector3.Distance(mainCamera.transform.position,highlightRoom.transform.position + centerOffset) < 4)
                    {
                        dialogue.ZoomIntoRoom = true;
                        firstClickOnRoom = true;
                    }
                    Vector3 newCameraPosition =
                        highlightedRoomCenter + new Vector3(-cameraOffset, cameraOffset, cameraOffset);
                    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                        newCameraPosition,
                        cameraLerpSpeed * Time.deltaTime);

                    tempVector = Vector2.Lerp(new Vector2(mainCamera.orthographicSize, mainCamera.orthographicSize),
                        new Vector2(zoomAmount, zoomAmount), zoomSpeed * Time.deltaTime);
                    mainCamera.orthographicSize = tempVector.x;
                    foreach (GameObject room in roomManager.rooms)
                    {
                        if (room != highlightRoom)
                        {
                            Room tempRoom = room.GetComponent<Room>();
                            room.transform.position =
                                Vector3.Lerp(room.transform.position,
                                    new Vector3(tempRoom.spawnPosition.x, tempRoom.spawnPosition.y + 20,
                                        tempRoom.spawnPosition.z), roomMoveSpeed * Time.deltaTime);
                        }
                    }
                    outside.transform.position = Vector3.Lerp(outside.transform.position,new Vector3(outsideOrigin.x,outsideOrigin.y - 20,outsideOrigin.z),roomMoveSpeed*Time.deltaTime);
                }
                else if (highlightRoom == null)
                {
                    if (mainCamera.transform.position != mainCameraOrigin && !positionReseted)
                    {
                        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                            mainCameraOrigin, cameraLerpSpeed * Time.deltaTime);
                        if (Vector3.Distance(mainCamera.transform.position, mainCameraOrigin) < 2)
                        {
                            positionReseted = true;
                        }
                    }
                    roomManager.panAble = true;
                    tempVector = Vector2.Lerp(new Vector2(mainCamera.orthographicSize, mainCamera.orthographicSize),
                        new Vector2(originalZoom, originalZoom), zoomSpeed * Time.deltaTime);
                    mainCamera.orthographicSize = tempVector.x;
                    foreach (GameObject room in roomManager.rooms)
                    {
                        if (room != highlightRoom)
                        {
                            Room tempRoom = room.GetComponent<Room>();
                            room.transform.position =
                                Vector3.Lerp(room.transform.position,
                                    new Vector3(tempRoom.spawnPosition.x, tempRoom.spawnPosition.y,
                                        tempRoom.spawnPosition.z),
                                    roomMoveSpeed * Time.deltaTime);
                        }
                    }
                    outside.transform.position = Vector3.Lerp(outside.transform.position,outsideOrigin,roomMoveSpeed*Time.deltaTime);
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
                    if (selection != null && selection.CompareTag("Dog"))
                    {
                        highlightDog = selection;
                        roomAssign.highlightDog = highlightDog;
                        if (!firstClick)
                        {
                            dialogue.ClickedDog = true;
                            firstClick = true;
                        }
                    }
                }
                else
                {
                    Debug.Log("hjdjgjdajgsj");
                    highlightRoom = null;
                    highlightDog = null;
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
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began ||
                touch.phase == TouchPhase.Stationary)
            {
                tempTime += Time.deltaTime;
            }

            if (touch.deltaPosition.magnitude > 0)
                touchMoved = true;
            
            if (touch.phase == TouchPhase.Ended)
            {
                if (!touchMoved) //tempTime < maxClickHold)
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

                        highlightDog = selection;
                        if (selection != null && selection.CompareTag("Dog"))
                        {
                            highlightDog = selection;
                            roomAssign.highlightDog = highlightDog;
                        }
                    }
                    else
                    {
                        highlightRoom = null;
                        highlightDog = null;
                    }

                    tempTime = 0f;
                }
                else
                {
                    tempTime = 0f;
                }

                touchMoved = false;
            }
        }
    }

    private void OnGUI()
    {
        if (roomManager.developerMode)
            GUI.Label(new Rect(10, 40, 200, 200), "TOUCH TIME: " + tempTime);
    }
}