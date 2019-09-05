using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] public GameObject prefab_Room;
    [Range(1,10)]
    [SerializeField] public int roomAmount = 1;

    [SerializeField] public bool developerMode = false;
    private createRooms _createRooms;
    
    // Start is called before the first frame update
    void Start()
    {
         _createRooms = gameObject.AddComponent<createRooms>();
         
    }
    
    private void OnGUI()
    {
        if (developerMode)
        {
            GUI.Label(new Rect(10, 10, 200, 200), "Touch count: " + Input.touchCount);
        }
    }
}
