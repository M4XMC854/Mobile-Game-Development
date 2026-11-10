using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// This script is responsible for managing the animations of the dice, the predetermined outcomes and resetting the dice to its original position.

public class RollManager : MonoBehaviour
{

    [SerializeField] GameObject Dice; // Reference to the Dice GameObject
    [SerializeField] AnimationClip[] RollAnimations; // Array to hold the roll animations
    Vector3 StartPos = new Vector3(0, -85, 0); // The starting position of the dice
    Vector3 StartRotation = new Vector3(270, 0, 0); // The starting rotation of the dice
    int Outcome; // Variable to hold the outcome of the dice roll
    public bool isRolling = false; // Flag to check if the dice is currently rolling

    private void Awake()
    {
        // Ensure the dice is at the starting position and rotation when the game starts
        Dice.transform.localPosition = StartPos;
        Dice.transform.localEulerAngles = StartRotation;

        Dice.GetComponent<Animator>().enabled = false;
    }

    public void PlayRollAnimation()
    {
        isRolling = true; // Set the rolling flag to true
        Outcome = Random.Range(1, 7); // Generate a random outcome between 1 and 6
        if (Outcome < 1 || Outcome > RollAnimations.Length)
        {
            Debug.LogError("Invalid outcome: " + Outcome);
            return;
        }
        AnimationClip SelectedAnimation = RollAnimations[Outcome - 1]; // Select the animation based on the outcome
        Animator DiceAnimator = Dice.GetComponent<Animator>(); // Get the Animator component of the dice

        Dice.GetComponent<Animator>().enabled = true; // Enable the Animator component of the dice
        DiceAnimator.Play(SelectedAnimation.name, 0, 0f); // Play the selected animation

        StartCoroutine(WaitToReset(SelectedAnimation.length)); // Start the coroutine to wait before resetting the dice
    }

    public void ResetDice()
    {
        Dice.transform.localPosition = StartPos; // Reset the position of the dice to its starting position
        Dice.transform.localEulerAngles = StartRotation; // Reset the rotation of the dice to its starting rotation
        isRolling = false; // Set the rolling flag to false
    }

    IEnumerator WaitToReset(float Duration)
    {
        yield return new WaitForSeconds(Duration + 0.3f); // Wait for the duration of the animation
        ResetDice(); // Reset the dice position and rotation
    }
}
