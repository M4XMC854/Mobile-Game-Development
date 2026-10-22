using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;


public class SettingsManager : MonoBehaviour
{

    [SerializeField] GameObject SettingsUI; // Reference to the settings UI
    [SerializeField] GameObject MainUI; // Reference to the main UI
    [SerializeField] GameObject VolumeSlider; // Reference to the volume slider
    [SerializeField] GameObject HapticFeedback; // Reference to the haptic feedback toggle
    bool HapticFeedbackEnabled; // Variable to store the state of haptic feedback
    [SerializeField] GameObject SwipeRoll; // Reference to the swipe to roll toggle
    bool SwipeRollEnabled; // Variable to store the state of swipe to roll
    [SerializeField] AudioMixer AudioMixer; // Reference to the audio mixer
    [SerializeField] DiceRoll DiceRollScript; // Reference to the DiceRoll script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HapticFeedbackEnabled = HapticFeedback.GetComponent<UnityEngine.UI.Toggle>().isOn; // Get the state of the haptic feedback toggle
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
        DiceRollScript.enabled = false; // Disable the DiceRoll script to prevent dice rolling while in settings
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false); // Hide the settings UI
        MainUI.SetActive(true ); // Display the main game UI
        DiceRollScript.enabled = true; // Enable the DiceRoll script to allow dice rolling again
    }

    public void SetVolume()
    {
        float Volume = VolumeSlider.GetComponent<UnityEngine.UI.Slider>().value; // Get the value of the volume slider
        float DB = Mathf.Log10(Mathf.Clamp(Volume, 0.0001f, 1f)) * 20f; // Convert the volume to decibels
        AudioMixer.SetFloat("MasterVolume", DB); // Set the volume of the audio mixer
    }

    public void SetHapticFeedback()
    {
        HapticFeedbackEnabled = HapticFeedback.GetComponent<UnityEngine.UI.Toggle>().isOn; // Get the state of the haptic feedback toggle
    }

    public void SetSwipeRoll()
    {
        SwipeRollEnabled = SwipeRoll.GetComponent<UnityEngine.UI.Toggle>().isOn; // Get the state of the swipe to roll toggle
    }
}

