using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DotsManager : MonoBehaviour
{

    public float Dots;
    int DotsRounded;
    public float DotMultipler;
    [SerializeField] TextMeshProUGUI DotsUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayDots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function calculates the dots to be awarded to the player based off the predetermined outcome
    public void CalculateDots(int Outcome) 
    {
        Dots = Dots + (Outcome * DotMultipler);
        DotsRounded = Mathf.RoundToInt(Dots);
    }

    public void DisplayDots()
    {
        DotsUI.text = DotsRounded.ToString();
    }
}
