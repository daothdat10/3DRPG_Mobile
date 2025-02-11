using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 6;

    private IList<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public event EventHandler<InventoryEventArgs> ItemUsed;



    // kho đồ
    public Inventory()
    {
        for(int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    // thêm item vào kho đồ
    private InventorySlot FindStackableSlot(InventoryItemBase item)
    {
        foreach(InventorySlot slot in mSlots)
        {

            if(slot.IsStackable(item))
                return slot;
        }
        return null;
    }


    // tìm đến các slot ô còn trống
    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.isEmpty)
                return slot;

        }

        return null;
    }



    //thêm item
    public void AddItem(InventoryItemBase item)
    {
        InventorySlot freeSlot = FindStackableSlot(item);
        if(freeSlot == null)
        {
            freeSlot = FindNextEmptySlot();
        }
        if(freeSlot != null)
        {
            freeSlot.AddItem(item);
            if(ItemAdded != null)
            {
                ItemAdded(this,new InventoryEventArgs(item));
            }
        }
    }

    // xóa item
    public void RemoveItem(InventoryItemBase item)
    {

        foreach(InventorySlot slot in mSlots)
        {
            if (slot.Remove(item))
            {
                if(ItemRemoved != null)
                {
                    ItemRemoved(this,new InventoryEventArgs(item));
                }
                break;
            }
        }
    }
    
    //sử dụng items
    internal void UseItem(InventoryItemBase item)
    {
        
            if (ItemUsed != null)
            {
                ItemUsed(this, new InventoryEventArgs(item));
            }
       
            // Logic sử dụng item khác (nếu cần)
            item.OnUse();
        
    }

}
