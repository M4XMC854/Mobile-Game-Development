using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] LevelSections; // Array of level section prefabs to spawn
    [SerializeField] private Transform Camera; // Reference to the main camera
    [SerializeField] private float SpawnX; // X position to spawn level sections
    [SerializeField] private float LevelHeight = 30f; // Height of each level section
    [SerializeField] private float SpawnBuffer; // Buffer distance to spawn new level sections ahead of the camera
    private float NextSpawnY; // Y position to spawn the next level section


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextSpawnY = 44; // Initial Y position to spawn the first level section
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.position.y + SpawnBuffer >= NextSpawnY) // Check if the camera is close enough to spawn a new level section
        {
            SpawnSection(); // Spawn a new level section
        }
    }

    void SpawnSection()
    {
        GameObject sectionToSpawn = LevelSections[Random.Range(0, LevelSections.Length)]; // Select a random level section prefab
        Vector3 spawnPos = new Vector3(SpawnX, NextSpawnY, 0); // Create the spawn position
        Instantiate(sectionToSpawn, spawnPos, sectionToSpawn.transform.rotation); // Spawn the selected level section at the spawn position

        NextSpawnY += LevelHeight; // Update NextSpawnY for the next spawn
    }
}
