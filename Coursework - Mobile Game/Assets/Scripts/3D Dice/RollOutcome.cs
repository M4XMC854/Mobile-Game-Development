using UnityEngine;
[System.Serializable]

public class RollOutcome
{
    public int Outcome;

    public void DetermineOutcome()
    {
        Outcome = Random.Range(1, 7);
    }
}
