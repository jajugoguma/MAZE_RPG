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

    public void Clickeditem(int index)
    {
        
        inventory.RemoveItem(index);
        RefreshInventory();
    }

    private EventDelegate.Parameter MakerParameter (Object value , System.Type type)
    {
        EventDelegate.Parameter param = new EventDelegate.Parameter();
        //Debug.Log("xxxx" + value);
        param.obj = value;
        param.expectedType = type;

        return param;
    }

    // 인벤토리에 있는 물품을 실제 UI로 띄우는 Func
    public void RefreshInventory()
    {
        itemSlotBG = transform.Find("InventoryBackGround");
        itemSlot = itemSlotBG.Find("itemSlot");

        foreach (Transform child in itemSlotBG)
        {
            if (child == itemSlot) continue;
            Destroy(child.gameObject);
            
        }

        int x = -150;
        int y = 100;
        float itemSlotCellSize = 0f;
        int index = 0;

        //UIButton button = itemSlot.GetComponent<UIButton>();
        //EventDelegate btnEvent = new EventDelegate(this, "Clickeditem");
        //button.onClick.Add(btnEvent);
        foreach (Item item in inventory.GetItemList())
        {
            //Debug.Log(index);
            UISprite tmpUisprite = itemSlot.GetComponent<UISprite>();
            tmpUisprite.spriteName = item.GetImage();

            UIButton button = itemSlot.GetComponent<UIButton>();
            object obj = index;
            button.onClick[0].parameters[0].value = obj;

            // btnEvent.parameters[0] = MakerParameter(, typeof(int);
            
            //btnEvent.parameters[0] = new EventDelegate.Parameter(obj);

            //EventDelegate.Add(button.onClick, btnEvent);
           
            
            

            RectTransform itemSlotRectTransform = Instantiate(itemSlot, itemSlotBG).GetComponent<RectTransform>();
            //Debug.Log(item.GetImage());
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x + itemSlotCellSize, y + itemSlotCellSize);

            

            x += 100;
            if (x > 150)
            {
                x = -150;
                y-=100;
            }
            index++;
        }

    }
}
