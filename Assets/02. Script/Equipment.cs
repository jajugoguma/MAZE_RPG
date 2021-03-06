﻿using System.Collections;
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
        if(null == item)
            return;
        equipmentList.Add(item);
        switch (item.itemType)
        {
            default:
            case Item.ItemType.Armor:
            case Item.ItemType.Helmet:
                player._playerData.max_hp += 20;
                player.currentHp += 20;
                break;
            case Item.ItemType.Sword:
                player._playerData.atk += 3;
                break;
        }
        player.uiEquipment.RefreshEquipment();
    }

    public void SettingEquipment(Item item)
    {
        if (null == item)
            return;
        item.OnAction(player);
        equipmentList.Add(item);
        player.uiEquipment.RefreshEquipment();
    }

    public List<Item> GetItemEquipment()
    {
        return equipmentList;
    }

    public void RemoveEquipment(Item item)
    {
        if (null == item)
            return;
        item.OnAction(player);
        equipmentList.Remove(item);

        switch (item.itemType)
        {
            default:
            case Item.ItemType.Armor:
            case Item.ItemType.Helmet:
                player._playerData.max_hp -= 20;
                player.currentHp -= 20;
                break;
            case Item.ItemType.Sword:
                player._playerData.atk -= 3;
                break;
        }

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
    
    
    
    /*
    public void EquipSaveCheck()
    {
        for (int i = 0; i <equipmentList.Count; i++)
        {
            switch (equipmentList[i].itemType)
            {
                default:
                case Item.ItemType.Helmet:
                    player._playerData.max_hp -= 20;
                    player.currentHp -= 20;
                    break;
                case Item.ItemType.Sword:
                    player._playerData.atk -= 3;
                    break;

            }
        }
    }
    */
}
