using UnityEngine;
using UnityEngine.Audio;


public class SettingsManager : MonoBehaviour
{

    [SerializeField] GameObject SettingsUI; // Reference to the settings UI
    [SerializeField] GameObject MainUI; // Reference to the main UI
    [SerializeField] GameObject VolumeSlider; // Reference to the volume slider
    [SerializeField] GameObject HapticFeedback; // Reference to the haptic feedback toggle
    [SerializeField] HapticManager HapticManager; // Reference to the Haptic manager script
    [SerializeField] GameObject SwipeRoll; // Reference to the swipe to roll toggle
    public bool SwipeRollEnabled; // Variable to store the state of swipe to roll
    [SerializeField] AudioMixer AudioMixer; // Reference to the audio mixer
    [SerializeField] RollInput RollInputScript; // Reference to the roll input script
    [SerializeField] GameObject Dice; // Reference to the Dice GameObject

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwipeRollEnabled = SwipeRoll.GetComponent<UnityEngine.UI.Toggle>().isOn; // Get the state of the swipe to roll toggle   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings()
    {
        SettingsUI.SetActive(true); // Displays the settings UI Screen
        MainUI.SetActive(false); // Disable the main UI elements
        RollInputScript.enabled = false; // Disable the RollInput script to prevent dice rolling while in settings
        Dice.SetActive(false); // Disable the Dice GameObject to prevent interaction while in settings
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false); // Hide the settings UI
        MainUI.SetActive(true ); // Display the main game UI
        RollInputScript.enabled = true; // Enable the RollInput script to allow dice rolling again
        Dice.SetActive(true); // Enable the Dice GameObject to allow interaction again
    }

    public void SetVolume()
    {
        float Volume = VolumeSlider.GetComponent<UnityEngine.UI.Slider>().value; // Get the value of the volume slider
        float DB = Mathf.Log10(Mathf.Clamp(Volume, 0.0001f, 1f)) * 20f; // Convert the volume to decibels
        AudioMixer.SetFloat("MasterVolume", DB); // Set the volume of the audio mixer
    }

    public void SetHapticFeedback()
    {
        HapticManager.SetHaptics(!HapticManager.HapticsEnabled);
    }

    public void SetSwipeRoll()
    {
        SwipeRollEnabled = SwipeRoll.GetComponent<UnityEngine.UI.Toggle>().isOn; // Get the state of the swipe to roll toggle
        RollInputScript.UpdateSwipeStatus(); // Update the swipe status in the RollInput script
    }
}

