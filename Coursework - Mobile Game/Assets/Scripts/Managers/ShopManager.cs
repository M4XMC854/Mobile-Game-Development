using System.Collections;
using UnityEngine;

public class ExpandableShop : MonoBehaviour
{
    Vector2 StartingPos; 
    Vector2 EndPos; 
    [SerializeField] AnimationCurve AnimationCurve; 
    [SerializeField] GameObject Canvas; 
    float CanvasHeight; 

    RectTransform RectTransform; 
    float EndTime; 
    bool IsOpen = false; 
    [SerializeField] GameObject Dice; 
    [SerializeField] RollManager RollManager; 


    void Awake()
    {
        CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height; 
        LastKeyFrame();
        RectTransform = GetComponent<RectTransform>();
        CalculatePositions();
    }

    private void Start()
    {
        RectTransform.anchoredPosition = StartingPos;
    }


    public void ToggleShop()
    {
        if (IsOpen == false && RollManager.isRolling == false)
        {
            StartCoroutine(Open());
        }
        else if (IsOpen == true)
        {
            StartCoroutine(Close());
        }
    }

    private IEnumerator Open() 
    {
        float ElapsedTime = 0;
        CalculatePositions(); 
        Dice.SetActive(false); 
        while (ElapsedTime < EndTime)
        {
            RectTransform.anchoredPosition = Vector3.Lerp(StartingPos, EndPos, AnimationCurve.Evaluate(ElapsedTime));
            yield return new WaitForEndOfFrame();
            ElapsedTime += Time.deltaTime;
        }
        IsOpen = true;
    }

    private IEnumerator Close() 
    {
        float ElapsedTime = 0;
        CalculatePositions(); 
        while (ElapsedTime < EndTime)
        {
            RectTransform.anchoredPosition = Vector3.Lerp(EndPos, StartingPos, AnimationCurve.Evaluate(ElapsedTime));
            yield return new WaitForEndOfFrame();
            ElapsedTime += Time.deltaTime;
        }
        Dice.SetActive(true); 
        IsOpen = false;
    }

    // This function is a makeshift fix for not being able to correctly position the shop according to different screen sizes
    void CalculatePositions()
    {
        CanvasHeight = Canvas.GetComponent<RectTransform>().rect.height; 

        if (CanvasHeight < 1650)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.01f + -CanvasHeight); 
        }
        else if (CanvasHeight >= 1650 && CanvasHeight < 2000)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.125f + -CanvasHeight); 
        }
        else if (CanvasHeight >= 2000 && CanvasHeight < 2400)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.21f + -CanvasHeight); 
        }
        else if (CanvasHeight >= 2400)
        {
            StartingPos = new Vector2(0, CanvasHeight * 0.3f + -CanvasHeight); 
        }

        EndPos = new Vector2(0, (-CanvasHeight / 2) + (CanvasHeight * 0.30f)); 
        Debug.Log("StartingPos: " + StartingPos + " EndPos: " + EndPos); 
    }

    // Finds the last key frame in the animation curve
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
        EndTime = MaxTime; 
    }

}
