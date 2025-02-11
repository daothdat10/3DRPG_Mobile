using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory inventory;

    private InventoryItemBase AttachedItem
    {
        get
        {
            ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
            return dragHandler.Item;
        }
    }

    public void OnItemclicked()
    {
        InventoryItemBase item = AttachedItem;
        if (item != null)
        {
            inventory.UseItem(item);
        }
    }
}
