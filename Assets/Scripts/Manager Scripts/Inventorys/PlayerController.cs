using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inventory inventory;

    public GameObject Hand;

    public HUD Hud;

    public pau_con_res playerCoins;
    
    private InventoryItemBase mCurrentItem = null;

    private void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    #region Inventory

    

    
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        InventoryItemBase item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent =null;
        
        if(item == mCurrentItem)
            mCurrentItem = null;

    }

    public void SetItemActive(InventoryItemBase item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }
    
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (e.Item.ItemType != EItemType.Consumble)
        {
            if(mCurrentItem != null)
                SetItemActive(mCurrentItem, false);
        }
        InventoryItemBase item = e.Item;
        SetItemActive(item, true);
        mCurrentItem = item;
    }

    public void DropCurrentItem()
    {
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        
        inventory.RemoveItem(mCurrentItem);
        
        Rigidbody rbItem =goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(Vector3.forward * 2.0f, ForceMode.Impulse);
            Invoke("DodropItem",0.25f);
        }
    }

    public void DropAndDestroyCurrentItem()
    {
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
        
        inventory.RemoveItem(mCurrentItem);
        
        Destroy(goItem);
        
        mCurrentItem = null;
    }

    public void DodropItem()
    {
        if (mCurrentItem != null)
        {
            Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());
            mCurrentItem = null;
            
        }
    }

    #endregion

    public void FixedUpdate()
    {
        if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
        {
            DropCurrentItem();
        }
    }

    public void Update()
    {
        InteractWithItem();
    }

  
    
    public void InteractWithItem()
    {
        if (mInteractItem != null)
        {
            
            mInteractItem.OnInteract();
            
            if (mInteractItem is InventoryItemBase)
            {
                InventoryItemBase inventoryItem = (mInteractItem as InventoryItemBase);
                inventory.AddItem(inventoryItem);
                inventoryItem.Onpickup();
                
                if(inventoryItem.UseItemAfterPickup)
                    inventory.UseItem(inventoryItem);
                
                mInteractItem = null;
            }
        }
        
    }

    private InteracableItemBase mInteractItem = null;
    public void OnTriggerEnter(Collider other)
    {
        TryInteraction(other);
    }

    public void TryInteraction(Collider other)
    {
        InteracableItemBase item = other.GetComponent<InteracableItemBase>();
        if (item != null)
        {
            if (item.CanInteract(other))
            {
                mInteractItem = item;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InventoryItemBase item = other.GetComponent<InventoryItemBase>();
        if(item != null)
            mInteractItem = null;
    }
    
}
