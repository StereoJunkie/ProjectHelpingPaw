using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundIngame : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FindObjectOfType<SoundManager>().Play("ButtonPress");
            }
        }
        if(Input.GetMouseButtonUp(0))
            FindObjectOfType<SoundManager>().Play("ButtonPress");
    }
}