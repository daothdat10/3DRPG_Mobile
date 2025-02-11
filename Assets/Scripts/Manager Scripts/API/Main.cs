using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;
    public DateTime? serverTime;
    
    public Web Web;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Web = GetComponent<Web>();
    }

    
}
