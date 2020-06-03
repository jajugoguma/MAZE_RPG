using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public enum ItemType
    {
        Sword,
        Helmet,
        Armor,
        Potion
    }

    public String GetImage()
    {
        switch (itemType) {
            default:
            case ItemType.Armor: return "armor";
            case ItemType.Sword: return "sword";
            case ItemType.Helmet: return "helmet";
            case ItemType.Potion: return "potion";
            }
    }


    public ItemType itemType;
    public bool isWearing = false;

}
