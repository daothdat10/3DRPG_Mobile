using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlowZombie : MonoBehaviour
{
    private Transform mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }
    private void Update()
    {
        transform.LookAt(mainCamera); 
    }
}
