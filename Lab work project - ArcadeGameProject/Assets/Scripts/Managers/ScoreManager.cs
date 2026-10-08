using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Transform PlayerTransform; // Reference to the player's Transform component
    [SerializeField] private Transform Plr2Transform; // Reference to Player 2's Transform component
    [SerializeField] private int PlayerHeight; // Player's height as an integer
    [SerializeField] private int Plr2Height; // Player 2's height as an integer
    private int Score = 0; // Player's score
    private int Plr2Score = 0; // Player 2's score
    private int HighScore; // Player's high score
    [SerializeField] private TextMeshProUGUI ScoreText; // Reference to the UI Text component to display the score
    [SerializeField] private TextMeshProUGUI Plr2ScoreText; // Reference to the UI Text component to display Player 2's score
    [SerializeField] private TextMeshProUGUI HighScoreText; // Reference to the UI Text component to display the high score

    // Start is called before the first frame update
    void Start()
    {
        Load(); // Load the high score at the start
        HighScoreText.text = "High Score: " + HighScore.ToString(); // Update the high score display
    }

    // Update is called once per frame
    void Update()
    {
        CalculateScore(); // Call the function to calculate and update the score
        CalculateScorePlr2(); // Call the function to calculate and update Player 2's score
    }

    void CalculateScore()
    {
        PlayerHeight = Mathf.FloorToInt(PlayerTransform.position.y); // Get the player's height as an integer
        PlayerHeight /= 3; // Divide the height by 3 to balance scoring
        if (PlayerHeight > Score)
        {
            Score = PlayerHeight; // Update score if the player has reached a new height
            ScoreText.text = "Player 1 Score: " + Score.ToString(); // Update the score display
        }
        Save(); // Save the score if it's a new high score
    }

    void CalculateScorePlr2()
    {
        Plr2Height = Mathf.FloorToInt(Plr2Transform.position.y); // Get Player 2's height as an integer
        Plr2Height /= 3; // Divide the height by 3 to balance scoring
        if (Plr2Height > Plr2Score)
        {
            Plr2Score = Plr2Height; // Update Player 2's score if they have reached a new height
            Plr2ScoreText.text = "Player 2 Score: " + Plr2Score.ToString(); // Update Player 2's score display
        }
    }

    public void Save()
    {
        if (Score > HighScore)
            PlayerPrefs.SetInt("HighScore", Score); // Save the highest score using PlayerPrefs
        PlayerPrefs.Save(); // Ensure the data is saved to disk
    }

    public void Load()
    {
        HighScore = PlayerPrefs.GetInt("HighScore"); // Load the highest score from PlayerPrefs
    }
}
