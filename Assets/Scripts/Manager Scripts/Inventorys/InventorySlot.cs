using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InventorySlot { 
    private Stack<InventoryItemBase> mItemStack = new Stack<InventoryItemBase>();

    private int mId = 0;

    public bool isEmpty
    {
        get { return Count == 0; }
    }
    public int Count
    {

        get { return mItemStack.Count; }
    }

    public InventorySlot(int id)
    {
        mId = id;
    }
    public int Id {  get { return mId; } }

    public void AddItem(InventoryItemBase item)
    {
        item.Slot = this;
        mItemStack.Push(item);
    }

    public InventoryItemBase FirstItem {
        get
        {
            if(isEmpty)
                   return null;

            return mItemStack.Peek();
        }
    }

    public bool IsStackable(InventoryItemBase item)
    {
        if (isEmpty)
            return false;

        InventoryItemBase first = mItemStack.Peek();

        if(first.Name == item.Name)
            return true;

        return false;
    }

  

    public bool Remove(InventoryItemBase item)
    {
        if (isEmpty)
        {
            return false;
        }

        InventoryItemBase first = mItemStack.Peek();
        if(first.Name == item.Name)
        {
            mItemStack.Pop();
            return true;
        }
        return false;

    }
}
