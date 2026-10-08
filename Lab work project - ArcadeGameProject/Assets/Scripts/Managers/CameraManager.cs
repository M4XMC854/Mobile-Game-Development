using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; // Speed at which the camera scrolls upwards
    private float CameraY; // Current Y position of the camera
    private float DeathY; // Y position at which the player dies
    [SerializeField] private Transform PlayerTransform; // Reference to the player's Transform component
    [SerializeField] private Rigidbody2D PlayerRB; // Reference to the player's RigidBody component
    [SerializeField] private Transform PlayerTransform2; // Reference to the player's Transform component
    [SerializeField] private Rigidbody2D PlayerRB2; // Reference to the player's RigidBody component 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraY += scrollSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, CameraY, transform.position.z);
        DeathY = CameraY - 15f; // Set DeathY to be 6 units below the camera's Y position
        if (PlayerTransform.position.y < DeathY && !TwoPlayerLoseCondition.Player1Dead)
        {
            TwoPlayerLoseCondition.Player1Dead = true; // Set Player 1 Dead condition to true
            PlayerRB.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
        }

        if (PlayerTransform2.position.y < DeathY && !TwoPlayerLoseCondition.Player2Dead)
        {
            TwoPlayerLoseCondition.Player2Dead = true; // Set Player 2 Dead condition to true
            PlayerRB2.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
        }

        if (TwoPlayerLoseCondition.Player1Dead == true && TwoPlayerLoseCondition.Player2Dead == true)
        {
            StartCoroutine(ReloadAfterDelay(2f)); // Reload the scene after a 2-second delay
        }

        IEnumerator ReloadAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
