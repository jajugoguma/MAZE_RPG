using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment 
{
    private List<Item> equipmentList;
    public Player player;
    public const int MaxItemCount = 3;
    
    public Equipment(Player player_)
    {
        player = player_;
        equipmentList = new List<Item>();
    }

    public void AddEquipment(Item item)
    {
        equipmentList.Add(item);
        player.uiEquipment.RefreshEquipment();
    }

    public List<Item> GetItemEquipment()
    {
        return equipmentList;
    }

    public void RemoveEquipment(Item item)
    {
        item.OnAction(player);
        equipmentList.Remove(item);
        player.uiInventory.RefreshInventory();

    }

    public bool IsEquip(Item.ItemType type_)
    {
        bool result = false;

        Item curItem = equipmentList.Find(find => find.itemType == type_);

        if (null != curItem)
            result = true;

        return result;
    }
}
