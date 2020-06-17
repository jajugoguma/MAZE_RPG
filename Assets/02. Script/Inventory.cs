using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{

    private List<Item> itemList;
    public Player player;
    public string inven;
    public string equip;

    public const int MaxItemCount = 8;

    public Inventory(Player player_)
    {
        player = player_;
        itemList = new List<Item>();

    }

    public void SettingInven(string inven, string equip)
    {
        string[] words = inven.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            for (Item.ItemType index = Item.ItemType.Sword; index < Item.ItemType.end; index++)
            {
                if (words[i].Equals(index.ToString()))
                {
                    AddItem(new Item { itemType = index });
                    break;
                }
            }
        }
        
        words.Initialize();
        words = equip.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Equals("1"))
                player.equipment.SettingEquipment(itemList[i]);       
               
        }
        player.uiInventory.RefreshInventory();

    }

    public string InvenSave()
    {
        inven = String.Empty;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (i == 0)
                inven = itemList[i].itemType.ToString();
            else
                inven = String.Concat(inven, " ", itemList[i].itemType.ToString());
        }


        return inven;
    }

    public string IsEquipSave()
    {
        equip = String.Empty;

        for (int i = 0; i < itemList.Count; i++)
        {
            if (i == 0)
            {
                if (itemList[i].isWearing)
                    equip = "1";
                else
                    equip = "0";
            }
                
            else
            {
                if(itemList[i].isWearing)
                   equip = String.Concat(equip, " 1");
                else
                   equip = String.Concat(equip, " 0");
            }

        }

        return equip;
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
            case Item.ItemType.Key:
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

    public bool HaveKey()
    {
        bool result = false;

        Item curItem = itemList.Find(find => find.itemType == Item.ItemType.Key);

        if (null != curItem)
            result = true;


        return result;
    }

    public void RemoveKey()
    {
        Item curItem = itemList.Find(find => find.itemType == Item.ItemType.Key);

        if (null != curItem)
            RemoveItem(curItem);

        player.uiInventory.RefreshInventory();
    }

}
