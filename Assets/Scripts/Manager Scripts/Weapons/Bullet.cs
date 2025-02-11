using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10;
    
    private Transform target;
    private float angle;
    
    private float aliveTimer = 5.0f;

    public Vector3 offset;
   
    public void Fire(Transform newTarget)
    {
        
        target = newTarget ;
        
        Destroy(gameObject, aliveTimer);
    }

    void Update()
    {
        
        if (target != null)
        {
            
            //tính toán vị trí đạn
            Vector3 targetPosition = target.position + offset;
            
            //tính toán hướng đạn 
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Tính toán góc quay dựa trên hướng đạn
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
             
        }
    }

    

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.layer)
        {
            case 9:
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                rb.AddForce(away*angle, ForceMode.Impulse);
                Destroy(gameObject);
                
                break;
                
        }
    }

    

    
    
    
}
