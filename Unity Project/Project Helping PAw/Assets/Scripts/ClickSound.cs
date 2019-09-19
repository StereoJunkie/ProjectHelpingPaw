using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource button;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).deltaTime < 0.2)
            {
                button.Play();
            }
        }
        if(Input.GetMouseButtonUp(0))
            button.Play();
    }
}
