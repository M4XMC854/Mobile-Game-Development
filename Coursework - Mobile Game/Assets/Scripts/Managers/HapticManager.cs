using UnityEngine;

public class HapticManager : MonoBehaviour
{
    public bool HapticsEnabled = true; // Variable to store the state of haptic feedback

    public void TriggerHaptic()
    {
        if (HapticsEnabled == false) // If haptics has been disabled do nothing
        {
            return;
        }
        if (HapticsEnabled == true) // If Haptics is turned on trigger a device vibration
        {
            Handheld.Vibrate(); 
        }
    }

    public void SetHaptics(bool Enabled)
    {
        HapticsEnabled = Enabled;
    }


}
