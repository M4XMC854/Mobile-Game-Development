using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] CloudPrefabs; // Array of cloud prefabs to spawn
    [SerializeField] private float MinX; // Minimum X position to spawn clouds
    [SerializeField] private float MaxX; // Maximum X position to spawn clouds
    [SerializeField] private float MinY;  // Minimum Y position to spawn clouds
    [SerializeField] private float MaxY;  // Maximum Y position to spawn clouds
    [SerializeField] private int SpawnAmount; // Number of clouds to spawn at each interval
    [SerializeField] private int SpawnInterval; // Time interval between spawns
    private float SpawnTimer; // Timer to track time since last spawn
    [SerializeField] private Transform Camera; // Reference to the main camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnInterval)
        {
            SpawnClouds();
            SpawnTimer = 0f;
        }
    }

    void SpawnClouds() 
    {
        MinY = Camera.position.y + 12f; // Update MinY based on camera position
        MaxY = Camera.position.y + 18f; // Update MaxY based on camera position
        for (int i = 0; i < SpawnAmount; i++)
        {
            float randomY = Random.Range(MinY, MaxY); // Generate a random Y position within the specified range
            float RandomX = Random.Range(MinX, MaxX); // Generate a random X position within the specified range
            Vector3 spawnPos = new Vector3(RandomX+ Camera.position.x, randomY, 0); // Create the spawn position relative to the camera
            int randomIndex = Random.Range(0, CloudPrefabs.Length); // Select a random cloud prefab from the array
            Instantiate(CloudPrefabs[randomIndex], spawnPos, CloudPrefabs[randomIndex].transform.rotation); // Spawn the selected cloud at the spawn position
        }
    }
}
