using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : InventoryItemBase
{

    public GameObject rocket;
    private GameObject tmpRocket;
    
    public weapon state = weapon.None;
    public AudioSource axeSound;
    
    private void Start()
    {
        state = weapon.None;
    }

    public override void OnUse()
    {
        base.OnUse();
        state = weapon.rocket;

    }


    public override void OnDrop()
    {
        base.OnDrop();
        state = weapon.None;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == weapon.rocket)
        {
            lunchRocket();
        }
    }

    void lunchRocket()
    {
        
        
        // tìm đối tượng enemy để đạn tự tìm tới
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocket, transform.position  + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<Bullet>().Fire(enemy.transform);
        }
    }

    
}

