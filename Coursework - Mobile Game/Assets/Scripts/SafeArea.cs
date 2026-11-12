using UnityEngine;

public class SafeArea : MonoBehaviour
{

    RectTransform RectTransform;
    Rect LastSafeArea; 

    void Start()
    {
        RectTransform = GetComponent<RectTransform>(); 
        ApplySafeAre(); 
    }

    void Update()
    {
        if (LastSafeArea != Screen.safeArea) 
        {
            ApplySafeAre();
        }
    }

    // Calculates and applies the safe area for the device
    void ApplySafeAre()
    {
        Rect SafeArea = Screen.safeArea; 

        Vector2 AnchorMin = SafeArea.position; 
        Vector2 AnchorMax = SafeArea.position + SafeArea.size; 

        AnchorMin.x /= Screen.width; 
        AnchorMin.y /= Screen.height; 

        AnchorMax.x /= Screen.width; 
        AnchorMax.y /= Screen.height; 

        RectTransform.anchorMin = AnchorMin; 
        RectTransform.anchorMax = AnchorMax; 

        LastSafeArea = SafeArea; 
    }


}
