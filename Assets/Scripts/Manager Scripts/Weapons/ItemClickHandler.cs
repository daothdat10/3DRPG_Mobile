using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;
    public void OnItemclicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

        IIventoryItem item = dragHandler.Item;

        Debug.Log(item.Name);

        _Inventory.UseItem(item);

        item.OnUse();
    }
}
