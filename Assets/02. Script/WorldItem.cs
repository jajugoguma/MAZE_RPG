using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem
{
    private List<Item> itemList;
    public Player player;

    public const int MaxItemCount = 8;

    public WorldItem(Player player_)
    {
        player = player_;
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        //player.uiInventory.RefreshInventory();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItem(Item item)
    {


    }
}
