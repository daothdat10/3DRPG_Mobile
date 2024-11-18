using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider health;

    public void UpdateHealthbar(float currenValue,  float maxValue) 
    {
        health.value = currenValue/maxValue;
    }
    void Update()
    {
        
    }
}
