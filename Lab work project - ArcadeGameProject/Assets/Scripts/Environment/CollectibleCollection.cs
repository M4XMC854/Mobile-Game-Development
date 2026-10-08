using TMPro;
using UnityEngine;

public class CollectibleCollection : MonoBehaviour
{
    private int TotalCollectibles; // Total number of collectibles the player has
    [SerializeField] private TextMeshProUGUI CollectibleScoreTxt; // Reference to the UI Text component to display the collectible count

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Load(); // Load the total collectibles at the start
        CollectibleScoreTxt.text = "" + TotalCollectibles.ToString(); // Update the collectible count display
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible")) // Check if the collided object is tagged as "Collectible"
        {
            Destroy(other.gameObject); // Remove the collectible from the scene
            TotalCollectibles += 1; // Increase the collectible count by 1
            CollectibleScoreTxt.text = "" + TotalCollectibles.ToString(); // Update the collectible count display
            Save(); // Save the updated collectible count
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Collectibles",TotalCollectibles); // Save the collectibles total using PlayerPrefs
        PlayerPrefs.Save(); // Ensure the data is saved to disk
    }

    public void Load()
    {
        TotalCollectibles = PlayerPrefs.GetInt("Collectibles"); // Load the collectibles total from PlayerPrefs
    }
}
