using Unity.VisualScripting;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{

    [SerializeField] SettingsManager settingsManager; // Reference to the settings manager script
    [SerializeField] DiceRoll DiceRoll; // Reference to the Dice roll script
    bool SwipeStatus; // Stores the current status of the swipe to roll setting

    float MinSwipeDistance  = 50f; // Minimum distance for a swipe to be recognized
    Vector2 StartPos; // Start position of the swipe
    Vector2 EndPos; // End position of the swipe
    Vector2 Swipe; // Swipe vector

    void Start()
    {
        SwipeStatus = settingsManager.SwipeRollEnabled; // Initialize the swipe status based on the settings manager
    }

    void Update()
    {
        OnSwipe();
    }

    public void UpdateSwipeStatus()
    {
        SwipeStatus = settingsManager.SwipeRollEnabled; // Update the swipe status based on the settings manager
    }

    void OnSwipe()
    {
        if (SwipeStatus == true && DiceRoll.isRolling == false)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0); // Gets primary touch

                if (t.phase == TouchPhase.Began)
                    StartPos = t.position; // Initialise the start pos at the fingers inital position

                if (t.phase == TouchPhase.Ended)
                {
                    EndPos = t.position; // Set the end position at the fingers final position
                    Swipe = EndPos - StartPos; // Calculate the swipe vector

                    if (Swipe.magnitude > MinSwipeDistance)
                    {
                        StartCoroutine(DiceRoll.RollDice()); // Roll the dice if a swipe is detected
                    }
                }
            }
        }
        else
        {
            return; // If swipe to roll is disabled, do nothing
        }
    }

}
