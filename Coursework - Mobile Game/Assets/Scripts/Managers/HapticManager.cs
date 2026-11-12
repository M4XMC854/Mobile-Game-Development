using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public bool HapticsEnabled = true;

    public void TriggerHaptic()
    {
        if (HapticsEnabled == false) 
        {
            return;
        }
        if (HapticsEnabled == true) 
        {
            Handheld.Vibrate(); 
        }
    }

    public void SetHaptics(bool Enabled)
    {
        HapticsEnabled = Enabled;
    }

}
