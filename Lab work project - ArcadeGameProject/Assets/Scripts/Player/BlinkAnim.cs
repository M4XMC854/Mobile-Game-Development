using System.Collections;
using UnityEngine;

public class BlinkAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BlinkRenderer;
    [SerializeField] private float MinBlinkTime = 2.0f;
    [SerializeField] private float MaxBlinkTime = 5.0f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinBlinkTime, MaxBlinkTime)); // Wait for a random time before blinking
            BlinkRenderer.sortingOrder = 2; // Bring to front
            yield return new WaitForSeconds(0.5f); // Blink duration
            BlinkRenderer.sortingOrder = -1; // Send to back
        }
    }
}
