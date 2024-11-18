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



    public Inventory()
    {
        for(int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    private InventorySlot FindStackableSlot(IIventoryItem item)
    {
        foreach(InventorySlot slot in mSlots)
        {

            if(slot.IsStackable(item)) return slot;
        }
        return null;
    }


    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.isEmpty)
                return slot;

        }

        return null;
    }



    public void AddItem(IIventoryItem item)
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

    public void RemoveItem(IIventoryItem item)
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

    internal void UseItem(IIventoryItem item)
    {
        if (ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }
        item.OnUse();
    }
}
