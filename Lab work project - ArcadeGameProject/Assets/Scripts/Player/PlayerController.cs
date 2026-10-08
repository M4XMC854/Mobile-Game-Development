using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovementForTesting : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float YBaseValue; // sets the base Y jump force of the player
    [SerializeField] private float YJumpForce; //sets the Y jump force of the player
    [SerializeField] private float XJumpForce; //sets the X jump force of the player
    private InputAction JumpAction; // Stores the reference to the Jump input action from the Input System
    private InputAction JumpActionPlr2; // Stores the reference to the Jump input action for Player 2

    [Header("WallDetection")]
    private bool isWalledLeft; // Indicates whether the player is currently against a left wall
    private bool isWalledRight; // Indicates whether the player is currently against a right wall
    [SerializeField] private Transform wallCheckLeft; // Transform of empty object used to check for left wall collisions
    [SerializeField] private Transform wallCheckRight; // Transform of empty object used to check for right wall collisions

    [Header("Dynamic Jump")]
    private float InitialYValue = 0; // Stores the initial Y value of the player when first hitting the wall
    private float FinalYValue; // Stores the final Y value of the player when leaving the wall
    private float YDifference; // Stores the difference between the initial and final Y values
    [SerializeField] private float wallSlidingSpeed = 0.1f; // sets the speed at which the player slides down the wall

    [SerializeField] private Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    [SerializeField] private LayerMask wallLayer; // Layer mask to identify wall objects

    [Header("UpwardWall")]
    [SerializeField] private float UpwardSlideSpeed;

    [Header("IceWall")]
    [SerializeField] private float IceSlideSpeed;

    [Header("Audio")]
    [SerializeField] private AudioSource jumpSound; // Reference to the AudioSource component for jump sound
    [SerializeField] private AudioClip jumpSFX; // Reference to the AudioClip for jump sound


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics2D.gravity = new Vector2(0f, -9.8f); // Sets the gravity
        JumpAction = InputSystem.actions.FindAction("Jump"); // Finds the Jump action from the Input System
        JumpActionPlr2 = InputSystem.actions.FindAction("JumpPlr2"); // Finds the Jump action for Player 2 from the Input System
        if (CompareTag("Player1"))
        {
            TwoPlayerLoseCondition.Player1Dead = false; // Set Player 1 lost condition to false at the start
        }
        if (CompareTag("Player2"))
        {
            TwoPlayerLoseCondition.Player2Dead = false; // Set Player 2 lost condition to false at the start
        }
    }

    IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Reloads the Main Menu scene
    }

    // Update is called once per frame
    void Update()
    {
        Jump(); // Call the Jump function to handle jumping
        WoodWall(); // Call the WoodWall function to handle wall sliding
        UpwardWall(); // Call the UpwardWall function to handle upward wall sliding
        BounceWall(); // Call the BounceWall function to handle bouncing off walls
        IceWall(); //Call the IceWall function to handle sliding faster on Ice walls
        Spike(); // Call the Spike function to handle spike wall collisions
        if (TwoPlayerLoseCondition.Player1Dead == true && TwoPlayerLoseCondition.Player2Dead == true)
        {
            StartCoroutine(ReloadAfterDelay(2f)); // Reload the scene after a 2 second delay if both players are dead
        }
    }

    void WoodWall()
    {
        //Check if the player is touching a left wall
        Collider2D HitLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        isWalledLeft = HitLeft != null;

        //Check if the player is touching a right wall
        Collider2D HitRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, wallLayer);
        isWalledRight = HitRight != null;

        if (HitLeft || HitRight != null)
        {
            if (isWalledLeft && HitLeft.CompareTag("Wood"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue)); // Slide down the wall
            }

            if (isWalledLeft && InitialYValue == 0)
            {
                InitialYValue = transform.position.y; // Store the initial Y value when hitting the wall
            }

            if (isWalledRight && HitRight.CompareTag("Wood"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue)); // Slide down the wall
            }

            if (isWalledRight && InitialYValue == 0)
            {
                InitialYValue = transform.position.y; // Store the initial Y value when hitting the wall
            }
        }
    }

    void Jump()
    {

        if (JumpAction.triggered && isWalledLeft && CompareTag("Player1")) // Jump diagonally off the left wall
        {
            FinalYValue = transform.position.y; // Store the final Y value when leaving the wall
            YDifference = InitialYValue - FinalYValue; // Calculate the difference in Y values
            YJumpForce = YJumpForce + YDifference; // Increase the Y jump force based on the distance slid down the wall
            rb.linearVelocity = new Vector2(XJumpForce, YJumpForce);
            PlayJumpSFX();
            YJumpForce = YBaseValue; // Reset YJumpForce to base value after jump
            InitialYValue = 0; // Reset InitialYValue for the next wall jump
        }

        // Jump diagonally off the right wall for Player 2
        if (JumpActionPlr2.triggered && isWalledLeft && CompareTag("Player2")) // Jump diagonally off the left wall
        {
            FinalYValue = transform.position.y; // Store the final Y value when leaving the wall
            YDifference = InitialYValue - FinalYValue; // Calculate the difference in Y values
            YJumpForce = YJumpForce + YDifference; // Increase the Y jump force based on the distance slid down the wall
            rb.linearVelocity = new Vector2(XJumpForce, YJumpForce);
            PlayJumpSFX();
            YJumpForce = YBaseValue; // Reset YJumpForce to base value after jump
            InitialYValue = 0; // Reset InitialYValue for the next wall jump
        }

        if (JumpAction.triggered && isWalledRight && CompareTag("Player1")) // Jump diagonally off the right wall
        {
            FinalYValue = transform.position.y; // Store the final Y value when leaving the wall
            YDifference = InitialYValue - FinalYValue; // Calculate the difference in Y values
            YJumpForce = YJumpForce + YDifference; // Increase the Y jump force based on the distance slid down the wall
            rb.linearVelocity = new Vector2(-XJumpForce, YJumpForce);
            PlayJumpSFX();
            YJumpForce = YBaseValue; // Reset YJumpForce to base value after jump
            InitialYValue = 0; // Reset InitialYValue for the next wall jump
        }

        if (JumpActionPlr2.triggered && isWalledRight && CompareTag("Player2")) // Jump diagonally off the right wall
        {
            FinalYValue = transform.position.y; // Store the final Y value when leaving the wall
            YDifference = InitialYValue - FinalYValue; // Calculate the difference in Y values
            YJumpForce = YJumpForce + YDifference; // Increase the Y jump force based on the distance slid down the wall
            rb.linearVelocity = new Vector2(-XJumpForce, YJumpForce);
            PlayJumpSFX();
            YJumpForce = YBaseValue; // Reset YJumpForce to base value after jump
            InitialYValue = 0; // Reset InitialYValue for the next wall jump
        }

        if (!JumpAction.triggered && !isWalledRight && !isWalledLeft)
        {
            InitialYValue = 0; // Reset InitialYValue if player slides off wall without jumping
        }
    }

    void UpwardWall()
    {
        //Check if the player is touching a left wall
        Collider2D HitLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        isWalledLeft = HitLeft != null;

        //Check if the player is touching a right wall
        Collider2D HitRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, wallLayer);
        isWalledRight = HitRight != null;

        if (HitLeft || HitRight != null)
        {
            if (isWalledLeft && HitLeft.CompareTag("Upward"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, UpwardSlideSpeed, float.MaxValue)); // Climsb up the wall
            }
            if (isWalledRight && HitRight.CompareTag("Upward"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, UpwardSlideSpeed, float.MaxValue)); // Climsb up the wall
            }
        }
    }

    void BounceWall()
    {
        //Check if the player is touching a left wall
        Collider2D HitLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        isWalledLeft = HitLeft != null;

        //Check if the player is touching a right wall
        Collider2D HitRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, wallLayer);
        isWalledRight = HitRight != null;

        if (HitLeft != null)
        {
            if (isWalledLeft && HitLeft.CompareTag("Bounce"))
            {
                rb.linearVelocity = new Vector2(XJumpForce, YJumpForce); // Bounce off the wall diagonally to the right
                PlayJumpSFX();
            }
        }

        if (HitRight != null)
        {
            if (isWalledRight && HitRight.CompareTag("Bounce"))
            {
                rb.linearVelocity = new Vector2(-XJumpForce, YJumpForce); // Bounce off the wall diagonally to the left
                PlayJumpSFX();
            }
        }
    }

    void IceWall()
    {
        //Check if the player is touching a left wall
        Collider2D HitLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        isWalledLeft = HitLeft != null;

        //Check if the player is touching a right wall
        Collider2D HitRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, wallLayer);
        isWalledRight = HitRight != null;

        if (HitLeft || HitRight != null)
        {
            if (isWalledLeft && HitLeft.CompareTag("Ice"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -IceSlideSpeed, float.MaxValue)); // Slide down the wall
            }

            if (isWalledLeft && InitialYValue == 0)
            {
                InitialYValue = transform.position.y; // Store the initial Y value when hitting the wall
            }

            if (isWalledRight && HitRight.CompareTag("Ice"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -IceSlideSpeed, float.MaxValue)); // Slide down the wall
            }

            if (isWalledRight && InitialYValue == 0)
            {
                InitialYValue = transform.position.y; // Store the initial Y value when hitting the wall
            }
        }
    }

    void Spike()
    {
        //Check if the player is touching a left wall
        Collider2D HitLeft = Physics2D.OverlapCircle(wallCheckLeft.position, 0.1f, wallLayer);
        isWalledLeft = HitLeft != null;

        //Check if the player is touching a right wall
        Collider2D HitRight = Physics2D.OverlapCircle(wallCheckRight.position, 0.1f, wallLayer);
        isWalledRight = HitRight != null;

        if (HitLeft || HitRight != null)
        {
            if (isWalledLeft && HitLeft.CompareTag("Spike"))
            {
                if (CompareTag("Player1"))
                {
                    TwoPlayerLoseCondition.Player1Dead = true; // Set Player 1 Dead condition to true
                    rb.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
                }
                if (CompareTag("Player2"))
                {
                    TwoPlayerLoseCondition.Player2Dead = true; // Set Player 2 Dead condition to true
                    rb.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
                }
            }
            if (isWalledRight && HitRight.CompareTag("Spike"))
            {
                if (CompareTag("Player1"))
                {
                    TwoPlayerLoseCondition.Player1Dead = true; // Set Player 1 Dead condition to true
                    rb.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
                }
                if (CompareTag("Player2"))
                {
                    TwoPlayerLoseCondition.Player2Dead = true; // Set Player 2 Dead condition to true
                    rb.bodyType = RigidbodyType2D.Static; // Freeze the player's movement
                }
                if (TwoPlayerLoseCondition.Player1Dead && TwoPlayerLoseCondition.Player2Dead)
                {
                    StartCoroutine(ReloadAfterDelay(2f)); // Reload the scene after a 2 second delay if both players are dead
                }
            }
        }

    }

    void PlayJumpSFX()
    {
        if (jumpSound != null && jumpSFX != null)
        {
            jumpSound.PlayOneShot(jumpSFX); // Play the jump sound effect
        }
    }
}
