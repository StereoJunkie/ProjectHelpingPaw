using System;
using UnityEngine;
using System.Collections;
 
public class fpsDisplay : MonoBehaviour
{
    private float timePassed;
    private RoomManager roomManager;

    private void Start()
    {
        roomManager = GetComponent<RoomManager>();
    }

    void Update()
    {
        timePassed = 0f;
        timePassed += (Time.unscaledDeltaTime - timePassed) * 0.1f;
    }
 
    void OnGUI()
    {
        if (roomManager.developerMode)
        {
            int width = Screen.width;
            int height = Screen.height;
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, width, height * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = height * 2 / 100;
            style.normal.textColor = new Color(1f, 1f, 1f, 1.0f);
            float milliSecond = timePassed * 1000.0f;
            float fps = 1.0f / timePassed;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", milliSecond, fps);
            GUI.Label(rect, text, style);
        }
    }
}