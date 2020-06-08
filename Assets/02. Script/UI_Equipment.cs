using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{
    private Equipment equipment;
    [SerializeField]
    private UIGrid grid = null;
    [SerializeField]
    private List<SlotItemView> slotItemViewList = new List<SlotItemView>();


    public void SetEquipment(Equipment equipment)
    {
        this.equipment = equipment;
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
            Debug.Log("xxxxxxx");
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
