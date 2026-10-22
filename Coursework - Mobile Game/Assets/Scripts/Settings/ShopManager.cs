using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ExpandableShop : MonoBehaviour
{
    [SerializeField] Vector2 StartingPos; // Start position of the shop UI
    [SerializeField] Vector2 EndPos; // End position of the shop UI
    [SerializeField] AnimationCurve AnimationCurve; // Stores the UI animation curve

    RectTransform RectTransform; 
    float EndTime;
    bool IsOpen = false; // Store the state of the UI



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LastKeyFrame();
        RectTransform = GetComponent<RectTransform>();
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
        while (ElapsedTime < EndTime)
        {
            RectTransform.anchoredPosition = Vector3.Lerp(EndPos, StartingPos, AnimationCurve.Evaluate(ElapsedTime));
            yield return new WaitForEndOfFrame();
            ElapsedTime += Time.deltaTime;
        }
        IsOpen = false;
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
