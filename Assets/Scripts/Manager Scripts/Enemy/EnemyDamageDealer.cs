using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealDamage;

    [SerializeField] float weaponLength;
    [SerializeField] int weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canDealDamage && !hasDealDamage)
        {
            RaycastHit hit;
            int layerMark = 1 << 8;
            if(Physics.Raycast(transform.position,-transform.up,out hit,weaponLength,layerMark))
            {
                if (hit.transform.TryGetComponent(out HealthSystem health) )
                {
                    health.TakeDamage(weaponDamage);
                    health.HitVFX(hit.point);
                    hasDealDamage = true;
                }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealDamage = false;
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
