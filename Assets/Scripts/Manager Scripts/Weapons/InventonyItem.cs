using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IIventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    InventorySlot Slot { get; set; }

    void Onpickup();

    void OnDrop();

    void OnUse();


   
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IIventoryItem item)
    {
        Item = item;
    }
    public IIventoryItem Item;
    
}
