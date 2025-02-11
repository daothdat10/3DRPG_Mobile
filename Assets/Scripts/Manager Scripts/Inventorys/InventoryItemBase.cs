using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    Default,
    Consumble,
    Weapon
}


public class InteracableItemBase : MonoBehaviour
{

    public string Name;

    public Sprite Image;

    public EItemType ItemType;

    public virtual void OnInteract()
    {
        
    }

    public virtual bool CanInteract(Collider other)
    {
        return true;
    }
}

public class InventoryItemBase : InteracableItemBase
{
    public InventorySlot Slot { get; set; }

    public virtual void OnDrop()
    {
        RaycastHit hit = new RaycastHit();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);

            gameObject.transform.position = hit.point;

            gameObject.transform.eulerAngles = DropRotation;
        }
    }

    public virtual void Onpickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnUse()
    {

        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }

   
    public Vector3 PickPosition;

    public Vector3 PickRotation;

    public Vector3 DropRotation;

    public bool UseItemAfterPickup = false;

}


