#if UNITY_EDITOR 

using System.Collections;
using UnityEditor.Animations;
using UnityEditor;
using UnityEngine;

public class PhysicsRecorder : MonoBehaviour
{
    // Dice variables
    new Transform transform; 
    Rigidbody rb; 
    float Min = -7.5f;
    float Max = 7.5f;
    float Value; 

    // Animation variables
    public AnimationClip RollAnimation; 
    private GameObjectRecorder Recorder;
    int SnapshotCount = 0; 
    bool IsRecording = false; 
    bool HasMoved = false; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initalise the Dice
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(0f, 0f, 0f);

        // Initalise the recorder and assign it to the dice
        Recorder = new GameObjectRecorder(gameObject);
        Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }



    /* SaveClip() creates a new animation clip,
     gives it a new name,
     saves the recorder data to the clip,
     creates a path to save the clip,
     saves the clip at the specified path */
    void SaveClip()
    {
        if (SnapshotCount > 0 && Recorder != null)
        {
            
            AnimationClip clip = new AnimationClip(); 
            clip.name = "DiceRoll"; 

            Recorder.SaveToClip(clip);

            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Animations/Dice/DiceRoll.anim"); 

            AssetDatabase.CreateAsset(clip, path); 
            AssetDatabase.SaveAssets(); 
            AssetDatabase.Refresh(); 

            Debug.Log("Saved Animation Clip to: " + path); 
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (rb.linearVelocity == Vector3.zero && rb.angularVelocity == Vector3.zero && HasMoved == true)
        {
            IsRecording = false; 
            SaveClip();
            HasMoved = false; 
        }

        if (Input.GetKeyDown(KeyCode.R) && IsRecording == false)
        {
            transform.position = new Vector3(0f, 0f, 0f);

            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero; 

            if (transform.position.x == 0f && transform.position.y == 0f && transform.position.z == 0f)
            {
                StartCoroutine(Roll());
            }
        }

    }

    // Late Update is being used to record the snapshots of the dice movement at the end of every frame
    private void LateUpdate()
    {
        if (IsRecording == true)
        {
            Recorder.TakeSnapshot(Time.deltaTime); 
            SnapshotCount++; 
        }
        else
        {
            return;
        }
    }



    float RandomValue()
    {
        Value = Random.Range(Min, Max);

        return Value;
    }


    // A coroutine to apply the random force to the dice and begin the animation recording 
    IEnumerator Roll()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        yield return wait;

        rb.AddForce(new Vector3(RandomValue(), 7.5f, RandomValue()), ForceMode.Impulse);
        rb.AddTorque(new Vector3(RandomValue(), RandomValue(), RandomValue()), ForceMode.Impulse);

        yield return new WaitForFixedUpdate(); 

        SnapshotCount = 0; 
        IsRecording = true; 
        HasMoved = true; 
    }

}
#endif