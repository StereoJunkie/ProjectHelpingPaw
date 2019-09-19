using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> prefab_Rooms;
    [SerializeField] public GameObject outside_Prefab;
    [Range(1,10)]
    [SerializeField] public int roomAmount = 1;

    [SerializeField] public bool developerMode = false;
    [SerializeField] public bool panAble = true;
    [SerializeField] public int DogsAdopted = 0;
    [SerializeField] public int DogsKilled = 0;
    [SerializeField] public List<GameObject> rooms;
    [SerializeField] public List<GameObject> dogs;
    private createRooms _createRooms;

    void Start()
    {
         _createRooms = gameObject.AddComponent<createRooms>();
         rooms = new List<GameObject>();
         dogs = new List<GameObject>();
    }
    
    private void OnGUI()
    {
        if (developerMode)
        {
            //GUI.Label(new Rect(10, 10, 200, 200), "Touch count: " + Input.touchCount);
            
        }
    }
}
