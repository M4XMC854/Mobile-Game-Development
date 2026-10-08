using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform panel;
    Rect previousSafe;
    Vector2Int previousSize;
    bool applied;

    void Awake() => panel = GetComponent<RectTransform>();

    void OnEnable() => applied = false;

    void Update()
    {
        var size = new Vector2Int(Screen.width, Screen.height);
        if (size.x <= 0 || size.y <= 0) return;
        Rect safe = Screen.safeArea;
        if (applied && safe == previousSafe && size == previousSize) return;

        // TODO: calculate normalised anchorMin and anchorMax.
        // TODO: assign both anchors and reset both offsets to zero.

        previousSafe = safe;
        previousSize = size;
        applied = true;
    }
}