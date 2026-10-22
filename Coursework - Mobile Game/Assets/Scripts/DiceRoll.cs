using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DiceRoll : MonoBehaviour
{

    public int[] Outcomes; // Array to hold the outcomes of the dice rolls
    public float RollSpeed = 2f; // Speed at which the dice rolls
    int DisplayNum; // Variable to hold the number to display
    int FinalNum; // Variable to hold the final number after rolling
    public TextMeshProUGUI DiceDisplay; // Reference to the GameObject that displays the dice number
    bool isRolling = false; // Flag to check if the dice is currently rolling

    private InputAction InteractAction; // Input action for interacting with the dice

    void Awake()
    {
        InteractAction = InputSystem.actions.FindAction("Interact"); // Find the "Interact" action in the Input System
        if (InteractAction == null)
        {
            Debug.LogError("Interact action not found in Input System.");
        }
        else
        {
            InteractAction.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current != null)
        {
            int TouchID = Touchscreen.current.primaryTouch.touchId.ReadValue(); // Get the finger ID of the primary touch
            if (EventSystem.current.IsPointerOverGameObject(TouchID)) // Check if the touch is over a UI element
            {
                return; // If it is, exit the Update method to prevent rolling the dice
            }
            if (InteractAction.WasPressedThisFrame() && !isRolling)
            {
                StartCoroutine(RollDice()); // Start the RollDice coroutine when the Interact action is pressed
            }
        }
    }


    IEnumerator RollDice()
    {
        isRolling = true; // Set the rolling bool to true

        float RollTime = RollSpeed; // Set RollTime to the current RollSpeed
        while (RollTime > 0)
        {
            // Generate and display a random outcome from the Outcomes array
            int randomIndex = Random.Range(0, Outcomes.Length); // Get a random index from the Outcomes array
            DisplayNum = randomIndex; // Set DisplayNum to the randomly selected outcome
            DiceDisplay.text = Outcomes[DisplayNum].ToString(); // Update the text of the DiceDisplay GameObject

            // Decrease RollTime over time
            RollTime -= Time.deltaTime; // Decrease RollTime over time

            yield return null;
        }
        FinalNum = DisplayNum; 
        isRolling = false;
    }
}
