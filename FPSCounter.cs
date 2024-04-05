using UnityEngine;

/// <summary>
/// This script is for testing purpusoes only in order to optimize visual quality for a better user experience.
/// </summary>
public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    float updateInterval = 5f; // Set the update interval to 0.5 seconds

    void LateUpdate()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Check if the time to update has passed
        if (deltaTime > updateInterval)
        {
            deltaTime = 0.0f;
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 220, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 50;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
