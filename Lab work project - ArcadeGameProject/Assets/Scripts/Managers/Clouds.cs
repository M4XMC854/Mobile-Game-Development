using UnityEngine;

public class Clouds : MonoBehaviour
{
    private Transform Camera; // Reference to the main camera
    private float ForeYSpeed = 0.15f; // Speed of the fore Y cloud movement
    private float BackYSpeed = 0.6f; // Speed of the back Y cloud movement
    private float MidSYpeed = 0.3f; // Speed of the mid Y cloud movement
    private float ForeXSpeed = 0.1f; // Speed of the fore X cloud movement
    private float MidXSpeed = 0.05f; // Speed of the mid X cloud movement
    private float BackXSpeed = 0.025f; // Speed of the back X cloud movement
    private bool MoveRight; // Direction of cloud movement
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveRight = Random.value > 0.5f;
        Camera = GameObject.FindWithTag("MainCamera").transform; // Find and assign the main camera transform
    }

    // Update is called once per frame
    void Update()
    {
        float CameraY = Camera.position.y; // Get the camera's Y position
        float XSpeed = 0f;
        float YSpeed = 0f;
        if (CompareTag("Fore")) // Foreground cloud
        {
            XSpeed = ForeXSpeed;
            YSpeed = ForeYSpeed;
        }
        else if (CompareTag("Mid")) // Midground cloud
        {
            XSpeed = MidXSpeed;
            YSpeed = MidSYpeed;
        }
        else if (CompareTag("Back")) // Background cloud
        {
            XSpeed = BackXSpeed;
            YSpeed = BackYSpeed;
        }

        if (!MoveRight) 
        {
            XSpeed = -XSpeed;
        }

        transform.Translate(new Vector3(XSpeed, YSpeed, 0) * Time.deltaTime); // Move the cloud based on its speed and direction


        if (CameraY - 15f > transform.position.y) // If the cloud is below the camera view
        {
            Destroy(gameObject); // Destroy the cloud object
        }
    }
}
