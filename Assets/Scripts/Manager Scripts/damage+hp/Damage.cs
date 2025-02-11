using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum weapon
{
    None,
    rocket,
    knife,
    bow,
}
public class Damage : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealDamge;

    public weapon weaponState;
    public float weaponLength;
    public float weaponDamage;
    void Start()
    {
        
        

        hasDealDamge = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canDealDamage && weaponState == weapon.bow  )
        {

            RaycastHit hit;
            int layerMask = 1 << 9;
            if(Physics.Raycast(transform.position,-transform.up,out hit,weaponLength,layerMask))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealDamge.Contains(hit.transform.gameObject) ){
                    if (hit.collider.gameObject.layer == 9)
                    {
                        enemy.TakeDamage(weaponDamage);
                        enemy.HitVFX(hit.point);
                        hasDealDamge.Add(hit.transform.gameObject); 
                        Debug.Log(hit.collider.name);
                    }
                        
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
