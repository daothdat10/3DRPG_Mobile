using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;

        Inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        int index = -1;

        foreach(Transform slot in inventoryPanel)
        {
            index++;
            //ảnh
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);
            Image image = imageTransform.GetComponent<Image>();
            TextMeshProUGUI txtcount = textTransform.GetComponent<TextMeshProUGUI>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            //tìm những ô còn trống thêm vào
            if (index == e.Item.Slot.Id)
            {

                image.enabled=true;
                image.sprite = e.Item.Image;

                int itemCount = e.Item.Slot.Count;
                if (itemCount > 1)
                {
                    txtcount.text = itemCount.ToString();
                }
                else
                    txtcount.text = " ";

                itemDragHandler.Item = e.Item;



                break;
            }

        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");

        int index = -1;
        foreach (Transform slot in inventoryPanel)
        {
            index++;
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Transform textTransform = slot.GetChild(0).GetChild(1);
            TextMeshProUGUI txtcount = textTransform.GetComponent<TextMeshProUGUI>();
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if(itemDragHandler.Item == null)
            {
                continue;
            }
            if(e.Item.Slot.Id == index)
            {
                int itemCount = e.Item.Slot.Count;
                itemDragHandler.Item = e.Item.Slot.FirstItem;

                if (itemCount < 2)
                {
                    txtcount.text = " ";
                }
                else
                    txtcount.text = itemCount.ToString();
                if (itemCount == 0)
                {
                    image.enabled = false;

                    image.sprite = null;
                }
                break;
            }

           
        }
    }

   
}
