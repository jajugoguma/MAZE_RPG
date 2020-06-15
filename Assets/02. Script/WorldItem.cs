using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem
{
    private List<Item> itemList;
    public Player player;

    public const int MaxItemCount = 8;
    public void TestInsert()
    {
        AddItem(new Item { itemType = Item.ItemType.Potion });
        AddItem(new Item { itemType = Item.ItemType.Sword });
     //   AddItem(new Item { itemType = Item.ItemType.Potion });
       // AddItem(new Item { itemType = Item.ItemType.Sword });
       // AddItem(new Item { itemType = Item.ItemType.Helmet });
       // AddItem(new Item { itemType = Item.ItemType.Sword });
        //AddItem(new Item { itemType = Item.ItemType.Armor });
        //AddItem(new Item { itemType = Item.ItemType.Potion });
    }



    public WorldItem(Player player_)
    {
        player = player_;
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        player.uiWorldItem.ReSetUI(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

   
}
