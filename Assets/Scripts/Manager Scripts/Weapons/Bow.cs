using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bow : InventoryItemBase
{
    public GameObject arrowPrefab;
    private GameObject tmpArrow;
    private weapon _weaponState;

    private void Awake()
    {
        _weaponState = weapon.None;
    }

    public override void OnUse()
    {
        
        base.OnUse();
        _weaponState = weapon.bow;
       
    }

    public override void OnDrop()
    {
        base.OnDrop();
        _weaponState = weapon.None;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _weaponState == weapon.bow)
        {
            SpawnArrow();
        }
    }

    public void SpawnArrow()
    {
        foreach (var enemys in FindObjectsOfType<Enemy>())
        {
            tmpArrow = Instantiate(arrowPrefab,transform.position,Quaternion.identity);
            tmpArrow.GetComponent<Bullet>().Fire(enemys.transform);
        }
    }
}
