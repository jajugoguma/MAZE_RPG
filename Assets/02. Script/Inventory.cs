﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<Item> itemList;
    public Player player;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Potion });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Potion });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Armor });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Armor });
        AddItem(new Item { itemType = Item.ItemType.Potion });

    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItem(int index)
    {
        Debug.Log(index);
        
        Item item = itemList[index];
        item.OnAction(player);
        itemList.RemoveAt(index);
        
    }

}