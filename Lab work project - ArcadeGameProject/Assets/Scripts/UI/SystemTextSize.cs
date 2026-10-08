// Example calculation for one assigned TMP label:
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;

public class SystemTextSize : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] float baseSize = 24f;

    void OnEnable() => Apply();
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) Apply();
    }

    public void Apply()
    {
        if (label == null) return;
        float scale = AccessibilitySettings.fontScale;
        if (scale <= 0f) scale = 1f;
        label.fontSize = baseSize * scale;
        // Allow the surrounding layout to wrap, grow or scroll.
    }
}