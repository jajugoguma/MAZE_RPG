using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory 
{

    private List<Item> itemList;
    public Player player;
    public string inven;

    public const int MaxItemCount = 8;

    public Inventory(Player player_,String inven)
    {
        player = player_;
        itemList = new List<Item>();
        SettingInven(inven);
     }

    public void SettingInven(String inven)
    {
        string[] words = inven.Split(' ');
        for (int i =0; i < words.Length; i++)
        {
            /*
            switch (words[i])
            {
                default:
                    break;
                case "Armor":
                    player.worldItem.AddItem(new Item { itemType = Item.ItemType.Armor });
                    break;
                case "Potion":
                    player.worldItem.AddItem(new Item { itemType = Item.ItemType.Potion });
                    break;
                case "Helmet":
                    player.worldItem.AddItem(new Item { itemType = Item.ItemType.Helmet });
                    break;
                case "Sword":
                    player.worldItem.AddItem(new Item { itemType = Item.ItemType.Sword });
                    break;
            }
            */
            for(Item.ItemType index = Item.ItemType.Sword; index < Item.ItemType.end; index++)
            {
                if(words[i].Equals(index.ToString()))
                {
                    player.worldItem.AddItem(new Item { itemType = index });
                    break;
                }
            }
        }
    }

   public string InvenSave()
    {
        inven = String.Empty;
        for (int i = 0; i <itemList.Count; i++)
        {
            inven = String.Concat(inven, " ", itemList[i].itemType.ToString());
        }


        return inven;
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        player.uiInventory.RefreshInventory();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItem(Item item)
    {
        item.OnAction(player);
        switch (item.itemType)
        {
            case Item.ItemType.Armor:
            case Item.ItemType.Helmet:
            case Item.ItemType.Sword:

                if(false == player.equipment.IsEquip(item.itemType))
                    player.equipment.AddEquipment(item);
                else
                {
                    item.OnAction(player);
                }

                break;
            case Item.ItemType.Potion:
                itemList.Remove(item);
                break;
        }
        /*

        item.OnAction(player);
        if (item.isWearing == true)
            //if(player.equipment.GetItemEquipment().Find
            {
                player.equipment.AddEquipment(item);
            }

        itemList.Remove(item);
        */
    }

}
