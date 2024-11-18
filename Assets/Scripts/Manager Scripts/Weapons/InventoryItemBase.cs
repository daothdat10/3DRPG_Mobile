using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class InventoryItemBase : MonoBehaviour, IIventoryItem
{
    
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }

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
