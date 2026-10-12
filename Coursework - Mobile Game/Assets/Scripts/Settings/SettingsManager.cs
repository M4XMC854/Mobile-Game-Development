using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public GameObject SettingsUI; // Reference to the settings UI
    public GameObject MainUI; // Reference to the main UI

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenSettings()
    {
        SettingsUI.SetActive(true); // Displays the settings UI Screen
        MainUI.SetActive(false); // Disable the main UI elements
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false); // Hide the settings UI
        MainUI.SetActive(true ); // Display the main game UI
    }

}

