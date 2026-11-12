using System.Collections;
using UnityEngine;


/// <summary>
/// This script is responsible for managing the animations of the dice, the predetermined outcomes and resetting the dice to its original position.

public class RollManager : MonoBehaviour
{

    [SerializeField] GameObject Dice; 
    [SerializeField] AnimationClip[] RollAnimations; 
    Vector3 StartPos = new Vector3(0, -85, 0); 
    Vector3 StartRotation = new Vector3(270, 0, 0); 
    int Outcome; 
    public bool isRolling = false; 

    private void Awake()
    {
        Dice.transform.localPosition = StartPos;
        Dice.transform.localEulerAngles = StartRotation;

        Dice.GetComponent<Animator>().enabled = false;
    }


    // This function generates a predetermined outcome and then displays the roll animation based off that outcome
    public void PlayRollAnimation()
    {
        isRolling = true; 
        Outcome = Random.Range(1, 7); 
        if (Outcome < 1 || Outcome > RollAnimations.Length)
        {
            Debug.LogError("Invalid outcome: " + Outcome);
            return;
        }
        AnimationClip SelectedAnimation = RollAnimations[Outcome - 1]; 
        Animator DiceAnimator = Dice.GetComponent<Animator>(); 

        Dice.GetComponent<Animator>().enabled = true; 
        DiceAnimator.Play(SelectedAnimation.name, 0, 0f); 

        StartCoroutine(WaitToReset(SelectedAnimation.length));
    }


    // Resets dice position after roll animation. CURRENTLY DOES NOT WORK NEEDS FIXING!!!!!!
    public void ResetDice()
    {
        isRolling = false;
        Dice.transform.localPosition = StartPos; 
        Dice.transform.localEulerAngles = StartRotation; 
    }

    IEnumerator WaitToReset(float Duration)
    {
        yield return new WaitForSeconds(Duration + 0.3f); 
        ResetDice(); 
    }
}
