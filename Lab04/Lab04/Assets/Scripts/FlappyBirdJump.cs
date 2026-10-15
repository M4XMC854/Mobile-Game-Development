using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Responsive Flappy Bird-style vertical movement for a 3D player.
/// Attach to the same GameObject as a Rigidbody.
/// Press Space or click the left mouse button to flap.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FlappyBirdJump : MonoBehaviour
{
    [Header("Flap Feel")]
    [SerializeField, Min(0.1f)] private float flapVelocity = 6.5f;
    [SerializeField, Min(0.1f)] private float gravityStrength = 20f;
    [SerializeField, Min(1f)] private float fallingGravityMultiplier = 1.8f;
    [SerializeField, Min(1f)] private float maxFallSpeed = 12f;
    [SerializeField] private bool preserveHorizontalVelocity = true;

    [Header("Input")]
    [SerializeField] private bool allowMouseClick = true;

    [Header("Debug")]
    [SerializeField] private bool logPhysics = false;
    [SerializeField] private bool logInputChecks = false;

    private Rigidbody rb;
    private int flapCount;
    private bool warnedAboutKinematic;

    private void Awake()
    {
        Debug.Log($"[FlappyBirdJump] Awake on '{name}'. Looking for Rigidbody.", this);
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError($"[FlappyBirdJump] Rigidbody missing on '{name}'. Disabling script.", this);
            enabled = false;
            return;
        }

        // This script applies its own gravity so it can make falling snappier.
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        Debug.Log(
            $"[FlappyBirdJump] Ready. Custom gravity={gravityStrength}, " +
            $"fall multiplier={fallingGravityMultiplier}, flap velocity={flapVelocity}, " +
            $"max fall speed={maxFallSpeed}. Built-in Rigidbody gravity disabled.",
            this
        );
    }

    private void Start()
    {
        Debug.Log("[FlappyBirdJump] Controls: press Space or left-click to flap.", this);
    }

    private void Update()
    {
        bool keyPressed = Keyboard.current != null &&
                          Keyboard.current.spaceKey.wasPressedThisFrame;
        bool mousePressed = allowMouseClick &&
                            Mouse.current != null &&
                            Mouse.current.leftButton.wasPressedThisFrame;

        if (logInputChecks)
        {
            if (Keyboard.current == null)
                Debug.LogWarning("[FlappyBirdJump] No keyboard detected by the Input System.", this);

            if (allowMouseClick && Mouse.current == null)
                Debug.LogWarning("[FlappyBirdJump] Mouse click enabled, but no mouse detected.", this);
        }

        if (keyPressed || mousePressed)
        {
            Debug.Log($"[FlappyBirdJump] Input received. Space={keyPressed}, mouse={mousePressed}.", this);
            Flap();
        }
    }

    private void FixedUpdate()
    {
        if (rb == null || rb.isKinematic)
            return;

        Vector3 velocity = rb.linearVelocity;
        float gravityMultiplier = velocity.y < 0f ? fallingGravityMultiplier : 1f;

        // Apply custom gravity. Falling gets extra gravity for a tighter, less floaty feel.
        velocity.y -= gravityStrength * gravityMultiplier * Time.fixedDeltaTime;

        // Clamp downward speed so it remains controllable.
        if (velocity.y < -maxFallSpeed)
            velocity.y = -maxFallSpeed;

        rb.linearVelocity = velocity;

        if (logPhysics)
        {
            Debug.Log(
                $"[FlappyBirdJump] Physics tick: vertical speed={velocity.y:F2}, " +
                $"gravity multiplier={gravityMultiplier:F2}, position={transform.position}.",
                this
            );
        }
    }

    private void Flap()
    {
        if (rb == null)
        {
            Debug.LogError("[FlappyBirdJump] Flap cancelled: Rigidbody reference is missing.", this);
            return;
        }

        if (rb.isKinematic)
        {
            if (!warnedAboutKinematic)
            {
                Debug.LogError(
                    "[FlappyBirdJump] Flap cancelled: Rigidbody is kinematic. Disable Is Kinematic.",
                    this
                );
                warnedAboutKinematic = true;
            }
            return;
        }

        Vector3 velocity = rb.linearVelocity;
        float oldVerticalSpeed = velocity.y;

        // Set vertical speed directly for a consistent, immediate flap.
        velocity.y = flapVelocity;

        if (!preserveHorizontalVelocity)
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }

        rb.linearVelocity = velocity;
        flapCount++;

        Debug.Log(
            $"[FlappyBirdJump] FLAP #{flapCount}. Vertical speed changed from " +
            $"{oldVerticalSpeed:F2} to {velocity.y:F2}. Position={transform.position}.",
            this
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning(
            $"[FlappyBirdJump] Collision with '{collision.gameObject.name}' " +
            $"({collision.contactCount} contact(s)).",
            this
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[FlappyBirdJump] Trigger entered: '{other.gameObject.name}'.", this);
    }

    private void OnDisable()
    {
        Debug.Log($"[FlappyBirdJump] Script disabled on '{name}'.", this);
    }
}