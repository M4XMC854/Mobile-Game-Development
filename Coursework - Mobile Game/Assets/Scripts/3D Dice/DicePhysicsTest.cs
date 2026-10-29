using UnityEngine;

public class DicePhysicsTest : MonoBehaviour
{

    Rigidbody body; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.AddForce (new Vector3 (0f, 5f, 0f), ForceMode.Impulse); 
        body.AddTorque (new Vector3 (3f, 2f, 4f), ForceMode.Impulse);
    }


}
