using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotBG;
    private Transform itemSlot;




    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }



    // 인벤토리에 있는 물품을 실제 UI로 띄우는 Func
    public void RefreshInventory()
    {
        int x = -150;
        int y = 100;
        float itemSlotCellSize = 0f;
        itemSlotBG = transform.Find("InventoryBackGround");
        itemSlot = itemSlotBG.Find("itemSlot");
        
        foreach (Item item in inventory.GetItemList())
        {
            UISprite tmpUisprite = itemSlot.GetComponent<UISprite>();
            tmpUisprite.spriteName = item.GetImage();
            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotBG).GetComponent<RectTransform>();
            Debug.Log(item.GetImage());
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x + itemSlotCellSize, y + itemSlotCellSize);

            

            x += 100;
            if (x > 150)
            {
                x = -150;
                y-=100;
            }
        }

    }
}
