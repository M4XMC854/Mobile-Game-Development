using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ExpandableShop : MonoBehaviour
{
    Vector2 StartingPos; // Start position of the shop UI
    Vector2 EndPos; // End position of the shop UI
    [SerializeField] AnimationCurve AnimationCurve; // Stores the UI animation curve
    [SerializeField] GameObject Canvas; // Reference to the canvas of the shop UI
    float CanvasHeight; // Stores the height of the canvas

    RectTransform RectTransform;
    float EndTime;
    bool IsOpen = false; // Store the state of the UI



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height; // Get the height of the canvas
        LastKeyFrame();
        RectTransform = GetComponent<RectTransform>();
        CalculatePositions(); // Calculate the starting and ending positions of the shop UI
        RectTransform.anchoredPosition = StartingPos;
    }

    public void ToggleShop()
    {
        if (IsOpen == false)
        {
            StartCoroutine(Open());
        }
        else if (IsOpen == true)
        {
            StartCoroutine(Close());
        }
    }

    private IEnumerator Open() // Coroutine to animate the opening of the shop UI
    {
        float ElapsedTime = 0;
        CalculatePositions(); // Calculate the starting and ending positions of the shop UI
        while (ElapsedTime < EndTime)
        {
            RectTransform.anchoredPosition = Vector3.Lerp(StartingPos, EndPos, AnimationCurve.Evaluate(ElapsedTime));
            yield return new WaitForEndOfFrame();
            ElapsedTime += Time.deltaTime;
        }
        IsOpen = true;
    }

    private IEnumerator Close() // Coroutine to animate the closing of the shop UI
    {
        float ElapsedTime = 0;
        CalculatePositions(); // Calculate the starting and ending positions of the shop UI
        while (ElapsedTime < EndTime)
        {
            RectTransform.anchoredPosition = Vector3.Lerp(EndPos, StartingPos, AnimationCurve.Evaluate(ElapsedTime));
            yield return new WaitForEndOfFrame();
            ElapsedTime += Time.deltaTime;
        }
        IsOpen = false;
    }

    void CalculatePositions()
    {
        CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height; // Get the height of the canvas

        if (CanvasHeight < 1650)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.01f + -CanvasHeight); // Set the starting position of the shop UI to be below the screen 
        }
        else if (CanvasHeight >= 1650 && CanvasHeight < 2000)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.125f + -CanvasHeight); // Set the starting position of the shop UI to be below the screen 
        }
        else if (CanvasHeight >= 2000 && CanvasHeight < 2400)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.21f + -CanvasHeight); // Set the starting position of the shop UI to be below the screen 
        }
        else if (CanvasHeight >= 2400)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.3f + -CanvasHeight); // Set the starting position of the shop UI to be below the screen 
        }

        EndPos = new Vector2(0, (-CanvasHeight / 2) + (CanvasHeight * 0.30f)); // Set the end position of the shop UI to be at the top of the screen
        Debug.Log("StartingPos: " + StartingPos + " EndPos: " + EndPos); // Log the starting and ending positions of the shop UI
    }

    void LastKeyFrame()
    {
        float MaxTime = Mathf.NegativeInfinity;
        foreach (Keyframe Frame in AnimationCurve.keys)
        {
            if(Frame.time > MaxTime) 
            {
                MaxTime = Frame.time; 
            }
        }
        EndTime = MaxTime; // Set the MaxTime to the last keyframe
    }

}
