using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _fontSize;
    
    private float deltaTime = 0.0f;
    GUIStyle style = new GUIStyle();
    private int _width;
    private int _height;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _width = Screen.width;
        _height = Screen.height;    
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void OnGUI()
    {
        Rect rect = new Rect(72, 130, _width, _fontSize);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = _fontSize;
        style.normal.textColor = Color.black;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = "FPS: " + string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
        rect.y += rect.height;
        text = "FPS Limit: " + Application.targetFrameRate.ToString();
        GUI.Label(rect, text, style);
        rect.y += rect.height;
        text = "VSync: " + QualitySettings.vSyncCount.ToString();
        GUI.Label(rect, text, style);
        rect.y += rect.height;
        text = "Width: " + Screen.width.ToString() + " Height: " + Screen.height.ToString();
        GUI.Label(rect, text, style);
        
    }
}
