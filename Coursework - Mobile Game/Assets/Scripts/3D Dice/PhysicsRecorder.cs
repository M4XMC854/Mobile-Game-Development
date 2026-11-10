using System.Collections;
using UnityEditor.Animations;
using UnityEditor;
using UnityEngine;

public class PhysicsRecorder : MonoBehaviour
{
    new Transform transform; // Reference to the Transform component of the dice
    Rigidbody rb; // Reference to the Rigidbody component of the dice
    float min = -7.5f; // Minimum value for random force and torque applied to the dice
    float max = 7.5f; // Maximum value for random force and torque applied to the dice
    float value; // Variable to store the random value generated for force and torque

    public AnimationClip RollAnimation; // Reference to the animation clip for rolling the dice

    private GameObjectRecorder Recorder; // Recorder to capture the physics state of the dice

    bool isRecording = false; // Flag to indicate if recording is in progress
    bool hasMoved = false; // Flag to indicate if the dice has moved
    int SnapshotCount = 0; // Counter to keep track of the number of snapshots taken during recording



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initalise the Dice
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(0f, 0f, 0f);

        Recorder = new GameObjectRecorder(gameObject); // Create a new GameObjectRecorder for the dice
        Recorder.BindComponentsOfType<Transform>(gameObject, true); // Bind the Transform component of the dice to the recorder and include its children
    }


    void SaveClip()
    {

        if (Recorder == null || SnapshotCount == 0) // Check if the recorder is empty or if no snapshots were taken during recording
        { 
            return; 
        }

        if (SnapshotCount > 0)
        {

            AnimationClip clip = new AnimationClip(); // Create a new animation clip to store the recorded physics state of the dice
            clip.name = "DiceRoll"; // Set the name of the animation clip to "DiceRoll"

            // Save the recorded session to the clip.
            Recorder.SaveToClip(clip);

            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Animations/Dice/DiceRoll.anim"); // Generate a unique asset path for the new animation clip

            AssetDatabase.CreateAsset(clip, path); // Create the new animation asset
            AssetDatabase.SaveAssets(); // Save the changes to the asset database
            AssetDatabase.Refresh(); // Refresh the asset database to reflect the changes made

            Debug.Log("Saved Animation Clip to: " + path); // Log the path of the saved animation clip
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (rb.linearVelocity == Vector3.zero && rb.angularVelocity == Vector3.zero && hasMoved == true)
        {
            isRecording = false; // Stop the recording of the dice, The dice roll is over.
            SaveClip(); // Save the recorded physics state of the dice to the animation clip
            hasMoved = false; // Reset the hasMoved flag to false for the next roll
        }

        if (Input.GetKeyDown(KeyCode.R) && isRecording == false)
        {
            transform.position = new Vector3(0f, 0f, 0f);

            rb.linearVelocity = Vector3.zero; // Reset the linear velocity of the Rigidbody to zero
            rb.angularVelocity = Vector3.zero; // Reset the angular velocity of the Rigidbody to zero

            if (transform.position.x == 0f && transform.position.y == 0f && transform.position.z == 0f)
            {
                StartCoroutine(Roll());
            }
        }

    }


    private void LateUpdate()
    {
        if (isRecording == true)
        {
            Recorder.TakeSnapshot(Time.deltaTime); // Take a snapshot and record all the bindings values for this frame
            SnapshotCount++; // Increment the snapshot count to keep track of the number of snapshots taken during recording
        }
        else
        {
            return;
        }
    }


    float RandomValue()
    {
        value = Random.Range(min, max);

        return value;
    }



    IEnumerator Roll()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        yield return wait;

        rb.AddForce(new Vector3(RandomValue(), 7.5f, RandomValue()), ForceMode.Impulse);
        rb.AddTorque(new Vector3(RandomValue(), RandomValue(), RandomValue()), ForceMode.Impulse);

        yield return new WaitForFixedUpdate(); // Wait for the next physics update before starting the recording

        SnapshotCount = 0; // Reset the snapshot count to zero for the new recording session
        isRecording = true; // Set the recording flag to true to start recording the physics state of the dice
        hasMoved = true; // Set the hasMoved flag to true to indicate that the dice has moved
    }

}