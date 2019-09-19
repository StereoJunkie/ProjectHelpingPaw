using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundIngame : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).deltaTime < 0.2)
            {
                FindObjectOfType<SoundManager>().Play("ButtonPress");
            }
        }
        if(Input.GetMouseButtonUp(0))
            FindObjectOfType<SoundManager>().Play("ButtonPress");
    }
}