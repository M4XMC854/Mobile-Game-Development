using UnityEngine;

public class WallDespawner : MonoBehaviour
{
    [SerializeField] private Transform Camera; // Reference to the main camera
    private float CameraY; // Y position of the camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera").transform; // Find and assign the main camera transform
    }

    // Update is called once per frame
    void Update()
    {
        CameraY = Camera.position.y; // Get the camera's Y position
        if (CameraY - 15f > transform.position.y) // If the wall is below the camera view
        {
            Destroy(gameObject); // Destroy the wall object
        }
    }

}
