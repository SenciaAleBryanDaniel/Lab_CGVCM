using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void OnGUI()
    {
        float x = Screen.width / 2 - 10;
        float y = Screen.height / 2 - 10;
        GUI.Label(new Rect(x, y, 20, 20), "+");
    }
}