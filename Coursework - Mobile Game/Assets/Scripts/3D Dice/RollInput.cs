using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RollInput : MonoBehaviour
{

    [SerializeField] SettingsManager settingsManager; 
    [SerializeField] RollManager RollManager; 
    [SerializeField] HapticManager HapticManager; 


    bool SwipeStatus; 
    float MinSwipeDistance  = 50f; 
    Vector2 StartPos; 
    Vector2 EndPos; 
    Vector2 Swipe; 

    private InputAction InteractAction; 

    void Start()
    {
        SwipeStatus = settingsManager.SwipeRollEnabled;
    }

    void Awake()
    {
        InteractAction = InputSystem.actions.FindAction("Interact");
        if (InteractAction == null)
        {
            Debug.LogError("Interact action not found in Input System.");
        }
        else
        {
            InteractAction.Enable();
        }
    }

    void Update()
    {
        if (SwipeStatus == true)
        {
            OnSwipe(); 
        }
        else
        {
            OnTap(); 
        }
    }

    // Is called by settings manager to enable or disable swipe to roll
    public void UpdateSwipeStatus()
    {
        SwipeStatus = settingsManager.SwipeRollEnabled; 
    }


    // Detect and calculate a swipe and then roll the dice
    void OnSwipe()
    {
        if (SwipeStatus == false || RollManager.isRolling == true)
        {
            return;
        }

        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                StartPos = Touchscreen.current.primaryTouch.position.ReadValue(); 
            }
            else if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
            {
                EndPos = Touchscreen.current.primaryTouch.position.ReadValue(); 
                Swipe = EndPos - StartPos; 
                if (Swipe.magnitude >= MinSwipeDistance) 
                {
                    RollManager.PlayRollAnimation(); 
                    HapticManager.TriggerHaptic(); 
                }
            }
        }
    }

    // Detect a tap and roll the dice
    void OnTap()
    {
        if (SwipeStatus == false)
        {
            int TouchID = Touchscreen.current.primaryTouch.touchId.ReadValue(); 
            if (EventSystem.current.IsPointerOverGameObject(TouchID)) // Check if the touch is over a UI element
            {
                return; 
            }
            if (InteractAction.WasPressedThisFrame() && RollManager.isRolling == false)
            {
                RollManager.PlayRollAnimation(); 
                HapticManager.TriggerHaptic(); 
            }
         }
    }
}
