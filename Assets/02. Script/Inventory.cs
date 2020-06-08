using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{

    private List<Item> itemList;
    public Player player;

    public const int MaxItemCount = 8;

    public Inventory(Player player_)
    {
        player = player_;
        itemList = new List<Item>();
     }

    public void TestInsert()
    {
        AddItem(new Item { itemType = Item.ItemType.Potion });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Potion });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Helmet });
        AddItem(new Item { itemType = Item.ItemType.Sword });
        AddItem(new Item { itemType = Item.ItemType.Armor });
        //AddItem(new Item { itemType = Item.ItemType.Potion });
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
