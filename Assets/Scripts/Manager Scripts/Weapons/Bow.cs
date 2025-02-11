using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bow : InventoryItemBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField]private GameObject shootPoint;
    public Transform enemies;
    
    public float h = 5;
    public float g = -9.81f;

    private Vector3 velocityY;
    private Vector3 velocityXZ;
    
    private GameObject tmpArrow;
    private Ray ray;

    private weapon _weaponState;

    private float bulletTime;
    private float between=2;
    
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
       bulletTime += Time.deltaTime;
       enemies = GameObject.FindWithTag("Enemy").transform;
       if (GameObject.FindWithTag("Enemy")==null)
       {
           return;
       }
       
        
        if (bulletTime > between)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _weaponState == weapon.bow)
            {


                Launch();
                bulletTime = 0;


            }
        }
    }

    void Launch()
    {
        var newObj = Instantiate(bulletPrefab,shootPoint.transform.position,Quaternion.identity);
        Rigidbody rb = newObj.GetComponent<Rigidbody>();
        rb.useGravity = true;
        Physics.gravity = Vector3.up * g;
        
        rb.velocity = CalculateLaunchVelocity(newObj.transform);
        print(CalculateLaunchVelocity(newObj.transform));
    }
   
    Vector3 CalculateLaunchVelocity(Transform bulletTransform)
    {
        
        Transform closestTransform = null;
        float closestDistance = Mathf.Infinity;
        
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(enemies.position, bulletTransform.transform.position);
            if (distance < closestDistance)
            {
                closestTransform = enemies;
                closestDistance = distance;
            }
            float distancementY = closestTransform.position.y - bulletTransform.transform.position.y;
            Vector3 distancementXZ = new Vector3(closestTransform.position.x - bulletTransform.transform.position.x, 0, closestTransform.position.z - bulletTransform.transform.position.z);
            // áp dụng các công thức đã phân tích ra để tìm vận tốc
            velocityY = Vector3.up * Mathf.Sqrt((-2 * g * h));
            velocityXZ = distancementXZ / (Mathf.Sqrt(-2 * h / g) + (Mathf.Sqrt(2 * (distancementY - h) / g)));
        }
        return velocityXZ + velocityY * -Mathf.Sign(g);
    }
}
