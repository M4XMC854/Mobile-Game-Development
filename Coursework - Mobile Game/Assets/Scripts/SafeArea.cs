using UnityEngine;

public class SafeArea : MonoBehaviour
{

    RectTransform RectTransform;
    Rect LastSafeArea; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform = GetComponent<RectTransform>(); // Get the RectTransform component of the GameObject this script is attached to
        ApplySafeAre(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (LastSafeArea != Screen.safeArea) // Check if the safe area has changed
        {
            ApplySafeAre(); // Apply the new safe area
        }
    }


    void ApplySafeAre()
    {
        Rect SafeArea = Screen.safeArea; // Get the safe area of the screen

        Vector2 AnchorMin = SafeArea.position; // Get the position of the safe area
        Vector2 AnchorMax = SafeArea.position + SafeArea.size; // Get the size of the safe area

        AnchorMin.x /= Screen.width; // Normalize the x position of the safe area
        AnchorMin.y /= Screen.height; // Normalize the y position of the safe area

        AnchorMax.x /= Screen.width; // Normalize the x size of the safe area
        AnchorMax.y /= Screen.height; // Normalize the y size of the safe area

        RectTransform.anchorMin = AnchorMin; // Set the anchor min of the RectTransform to the normalized position of the safe area
        RectTransform.anchorMax = AnchorMax; // Set the anchor max of the RectTransform to the normalized size of the safe area

        LastSafeArea = SafeArea; // Store the current safe area for future comparison
    }


}
