﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Equipment : MonoBehaviour
{
    private Equipment equipment;
    [SerializeField]
    private UIGrid grid = null;
    [SerializeField]
    private List<SlotItemView> slotItemViewList = new List<SlotItemView>();

    public UILabel speedLabel;
    public UILabel healthLabel;
    public UILabel damageLabel;
    public UILabel levpointLabel;
    public UILabel levelLabel;

    
    public void HealthButtonClicked()
    {
        //Debug.Log("damageButtonclicked");
        if (equipment.player.levPoint > 0)
        {
            equipment.player.levPoint -= 1;
            equipment.player.maxHp += 1;
            equipment.player.currentHp +=1;
        }
        ResetLabels();
    }


    public void SpeedButtonClicked()
    {
        //Debug.Log("speedButtonclicked");
        if (equipment.player.levPoint > 0)
        {
            equipment.player.levPoint -= 1;
            equipment.player.speed += 0.5f;
        }

        ResetLabels();

    }

    public void DamageButtonClicked()
    {
        //Debug.Log("damageButtonclicked");
        if (equipment.player.levPoint > 0)
        {
            equipment.player.levPoint -= 1;
            equipment.player.attackdamage += 1;
        }
        ResetLabels();

    }
    
    // 캐릭터 스텟 뿌리는 함수
    public void ResetLabels()
    {

        if (equipment == null)
            return;
        levelLabel.text = string.Format("Lev : {0}", equipment.player.level);
        levpointLabel.text = string.Format("Point : {0}", equipment.player.levPoint);
        speedLabel.text = string.Format("Speed : {0}",equipment.player.speed);


        if (equipment.IsEquip(Item.ItemType.Armor))
            if (equipment.IsEquip(Item.ItemType.Helmet))
                healthLabel.text = string.Format("MaxHp : {0}[755CB3](+{1})[-]", equipment.player.maxHp,40);
            else
                healthLabel.text = string.Format("MaxHp : {0}[755CB3](+{1})[-]", equipment.player.maxHp, 20);
        else if(equipment.IsEquip(Item.ItemType.Helmet))
            healthLabel.text = string.Format("MaxHp : {0}[755CB3](+{1})[-]", equipment.player.maxHp, 20);
        else
            healthLabel.text = string.Format("MaxHp : {0}", equipment.player.maxHp); 


        if (equipment.IsEquip(Item.ItemType.Sword))
            damageLabel.text = string.Format("DMG :{0}[755CB3](+{1})[-]", equipment.player.attackdamage, 3);
        else
            damageLabel.text = string.Format("DMG :{0}", equipment.player.attackdamage);
    }



    public void SetEquipment(Equipment equipment)
    {
        this.equipment = equipment;

        ResetLabels();

        RefreshEquipment();
    }

    public void ClickedItemObject(GameObject itemObj)
    {
        if (null == itemObj)
        {
            Debug.LogError("itemObj is null");
            return;
        }

        SlotItemView item = itemObj.GetComponent<SlotItemView>();

        equipment.RemoveEquipment(item.ItemInfo);
        RefreshEquipment();

        //Debug.Log(item.ItemInfo);
    }


    // 장비창에 있는 물품을 실제 UI로 띄우는 Func
    public void RefreshEquipment()
    {
        List<Item> equipmentList = equipment.GetItemEquipment();

        if (null == equipmentList)
            return;

        for (int i = 0; i < Equipment.MaxItemCount; i++)
        {
            if (null == slotItemViewList[i])
                continue;
            //Debug.Log("xxxxxxx");
            slotItemViewList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < equipmentList.Count; i++)
        {
            if (null == slotItemViewList[i])
                continue;

            if (null == equipmentList[i])
                continue;

            int slotIndex = GetSlotIndex(equipmentList[i].itemType);

            if(slotIndex >= 0 && slotIndex < slotItemViewList.Count)
            {
                slotItemViewList[slotIndex].SetItemInfo(equipmentList[i]);
                slotItemViewList[slotIndex].SetSprite(equipmentList[i].GetImage());
                slotItemViewList[slotIndex].SetBtnEvent(ClickedItemObject);
                slotItemViewList[slotIndex].gameObject.SetActive(true);
            }
        }
        ResetLabels();
        if (null != grid)
            grid.Reposition();
    }

    private int GetSlotIndex(Item.ItemType itemType)
    {
        int index = -1;
        switch (itemType)
        {
            case Item.ItemType.Helmet:
                index = 0;
                break;
            case Item.ItemType.Armor:
                index = 1;
                break;
            case Item.ItemType.Sword:
                index = 2;
                break;
        }

        return index;
    }


}
