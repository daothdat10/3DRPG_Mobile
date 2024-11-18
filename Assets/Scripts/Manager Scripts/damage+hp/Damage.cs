using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealDamge;

    public float weaponLength;
    public float weaponDamage;
    void Start()
    {
        canDealDamage = false;

        hasDealDamge = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canDealDamage)
        {

            RaycastHit hit;
            int layerMask = 1 << 9;
            if(Physics.Raycast(transform.position,-transform.up,out hit,weaponLength,layerMask))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealDamge.Contains(hit.transform.gameObject) ){
                    enemy.TakeDamage(weaponDamage);
                    enemy.HitVFX(hit.point);
                    hasDealDamge.Add(hit.transform.gameObject); 
                }
            }
        }
    }
    public void StartDealDamge()
    {
        canDealDamage = true;
        
        hasDealDamge.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position-transform.up*weaponLength);
    }
}
