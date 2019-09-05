using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 touchStart;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
                touchStart = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(touch.position);
                Camera.main.transform.position += direction;
            }
        }
    }
}
