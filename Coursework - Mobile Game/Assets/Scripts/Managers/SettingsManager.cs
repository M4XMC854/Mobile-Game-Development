using UnityEngine;
using UnityEngine.Audio;


public class SettingsManager : MonoBehaviour
{

    [SerializeField] GameObject SettingsUI; 
    [SerializeField] GameObject MainUI; 
    [SerializeField] GameObject VolumeSlider; 
    [SerializeField] GameObject HapticFeedback; 
    [SerializeField] GameObject SwipeRoll;
    [SerializeField] GameObject Dice;

    public bool SwipeRollEnabled; 

    [SerializeField] AudioMixer AudioMixer; 
    [SerializeField] RollInput RollInputScript;
    [SerializeField] HapticManager HapticManager;
    [SerializeField] RollManager RollManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwipeRollEnabled = SwipeRoll.GetComponent<UnityEngine.UI.Toggle>().isOn; 
    }

    public void OpenSettings()
    {
        if (RollManager.isRolling == false)
        {
            SettingsUI.SetActive(true);
            MainUI.SetActive(false);
            RollInputScript.enabled = false;
            Dice.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false); 
        MainUI.SetActive(true ); 
        RollInputScript.enabled = true; 
        Dice.SetActive(true); 
    }

    public void SetVolume()
    {
        float Volume = VolumeSlider.GetComponent<UnityEngine.UI.Slider>().value; 
        float DB = Mathf.Log10(Mathf.Clamp(Volume, 0.0001f, 1f)) * 20f; 
        AudioMixer.SetFloat("MasterVolume", DB); 
    }

    public void SetHapticFeedback()
    {
        HapticManager.SetHaptics(!HapticManager.HapticsEnabled);
    }

    public void SetSwipeRoll()
    {
        SwipeRollEnabled = SwipeRoll.GetComponent<UnityEngine.UI.Toggle>().isOn; 
        RollInputScript.UpdateSwipeStatus(); 
    }
}

