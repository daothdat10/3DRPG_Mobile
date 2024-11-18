using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : InventoryItemBase {
        
    public AudioSource axeSound;
    public override string Name
    {
        get
        {
            return "Gun2";
        }
    }

    public override void OnUse()
    {
        base.OnUse();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            axeSound.Play();
        }
    }

    
}

