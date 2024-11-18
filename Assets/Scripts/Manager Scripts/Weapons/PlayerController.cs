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

    private void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IIventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent =null;

    }
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IIventoryItem item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        goItem.transform.parent = Hand.transform;
        
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IIventoryItem item = hit.collider.GetComponent<IIventoryItem>();

        if(item != null) 
        {
            inventory.AddItem(item);
            hit.collider.gameObject.SetActive(false);
        }

       if(hit.collider.tag == "Coin")
        {
            playerCoins.CoinColected();
           
        }
    }

    
}
