using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotBG;
    private Transform itemSlot;

    [SerializeField]
    private UIGrid grid = null;
    [SerializeField]
    private List<SlotItemView> slotItemViewList = new List<SlotItemView>();

    private void OnEnable()
    {
        if (null != grid)
            grid.Reposition();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }
   
    public void ClickedItemObject(GameObject itemObj)
    {
        if(null == itemObj)
        {
            Debug.LogError("itemObj is null");
            return;
        }

        SlotItemView item = itemObj.GetComponent<SlotItemView>();

        inventory.RemoveItem(item.ItemInfo);
        RefreshInventory();
        
        
        //Debug.Log(item.ItemInfo);
    }
   
  
    // 인벤토리에 있는 물품을 실제 UI로 띄우는 Func
    public void RefreshInventory()
    {
        List<Item> itemList = inventory.GetItemList();

        if (null == itemList)
            return;

        for (int i = 0; i < Inventory.MaxItemCount; i++)
        {
            if (null == slotItemViewList[i])
                continue;

            slotItemViewList[i].gameObject.SetActive(false);
        }

        for (int i = 0;i < itemList.Count;i++)
        {
            if (null == slotItemViewList[i])
                continue;

            if (null == itemList[i])
                continue;

            if(itemList[i].isWearing == true)
                continue;

            slotItemViewList[i].SetItemInfo(itemList[i]);
            slotItemViewList[i].SetSprite(itemList[i].GetImage());
            slotItemViewList[i].SetBtnEvent(ClickedItemObject);
            slotItemViewList[i].gameObject.SetActive(true);
        }

        if (null != grid)
            grid.Reposition();
    }
}
