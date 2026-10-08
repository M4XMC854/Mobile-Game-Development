using System.Collections;
using UnityEngine;

public class CountdownWall : MonoBehaviour
{
    [SerializeField] private float DelayBeforeFlip = 2.0f; // Time in seconds before the wall flips
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DelayBeforeFlip -= Time.deltaTime; // Decrease the delay timer by the time elapsed since the last frame
        if (DelayBeforeFlip <= 0)
        {
            transform.Rotate(0, 180, 0); // Rotate the wall 180 degrees around the Y-axis
            DelayBeforeFlip = 2.0f; // Reset the delay timer to 3 seconds
        }
    }
}
