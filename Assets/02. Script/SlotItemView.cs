using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemView : MonoBehaviour
{
    [SerializeField]
    private UISprite sprite = null;
    [SerializeField]
    private UIEventListener eventListner = null;

    private Item itemInfo = null;

    public Item ItemInfo { get { return itemInfo; }  }

    public void SetSprite(string spriteName_)
    {
        if (null != sprite)
            sprite.spriteName = spriteName_;
    }
    
    public void SetBtnEvent(UIEventListener.VoidDelegate event_)
    {
        if (null != eventListner)
            eventListner.onClick = event_;
        
    }

    public void SetItemInfo(Item itemInfo_)
    {
        itemInfo = itemInfo_;
    }

    

}
