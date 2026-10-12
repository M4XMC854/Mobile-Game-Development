using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PanelSafeAreaFitter : MonoBehaviour
{
    RectTransform panel;
    Rect lastSafe;
    Vector2Int lastSize;
    void Awake() => panel = GetComponent<RectTransform>();

    void Update()
    {
        var size = new Vector2Int(Screen.width, Screen.height);
        if (Screen.safeArea == lastSafe && size == lastSize) return;
        lastSafe = Screen.safeArea;
        lastSize = size;
        Vector2 min = lastSafe.min, max = lastSafe.max;
        min.x /= size.x; min.y /= size.y;
        max.x /= size.x; max.y /= size.y;
        panel.anchorMin = min;
        panel.anchorMax = max;
        panel.offsetMin = panel.offsetMax = Vector2.zero;
    }
}
