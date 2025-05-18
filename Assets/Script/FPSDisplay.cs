using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime = Time.unscaledDeltaTime;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new();

        Rect rect = new(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = Color.white;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} fps", fps);
        GUI.Label(rect, text, style);
    }
}