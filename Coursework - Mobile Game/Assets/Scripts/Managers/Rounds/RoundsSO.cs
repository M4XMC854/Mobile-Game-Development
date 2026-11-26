using UnityEngine;

[CreateAssetMenu(fileName = "RoundsSO", menuName = "Scriptable Objects/RoundsSO")]
public class RoundsSO : ScriptableObject
{
    [Header("Round Number")]
    public int RoundNum;

    [Header("Rules")]
    public int TargetSixes;
    public int MaxRolls;
    // Decrease rolls by 5 ( atleast for now )

    [Header("Modifiers")]
    public bool ResetSixesOnMiss;
    public bool DecreaseStreakOnMiss;
    public bool RetainStreakOnMiss;

    [Header("Reward")]
    public int CoinsRewarded;
    // Coin progression will start at 10 and go up by the equation 10 * 1.2 to the power of the round we are on!
}
