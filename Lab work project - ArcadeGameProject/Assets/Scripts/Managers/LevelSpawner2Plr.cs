using UnityEngine;

public class LevelSpawner2Plr : MonoBehaviour
{
    [SerializeField] private GameObject[] LevelSections; // Array of level section prefabs to spawn
    [SerializeField] private Transform Camera; // Reference to the main camera
    [SerializeField] private float Plr1SpawnX; // X position to spawn level sections
    [SerializeField] private float Plr2SpawnX; // X position to spawn level sections
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
            SpawnSections(); // Spawn new level sections for both players
        }
    }

    void SpawnSections()
    {
        GameObject sectionToSpawn = LevelSections[Random.Range(0, LevelSections.Length)]; // Select a random level section prefab
        Vector3 spawnPos = new Vector3(Plr1SpawnX, NextSpawnY, 0); // Create the spawn position
        Instantiate(sectionToSpawn, spawnPos, sectionToSpawn.transform.rotation); // Spawn the selected level section at the spawn position

        GameObject sectionToSpawn2 = LevelSections[Random.Range(0, LevelSections.Length)]; // Select a random level section prefab
        Vector3 spawnPos2 = new Vector3(Plr2SpawnX, NextSpawnY, 0); // Create the spawn position
        Instantiate(sectionToSpawn2, spawnPos2, sectionToSpawn2.transform.rotation); // Spawn the selected level section at the spawn position

        NextSpawnY += LevelHeight; // Update NextSpawnY for the next spawn
    }

}
