using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
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


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor: return ItemAsset.Instance.armorSprite;
            case ItemType.Sword: return ItemAsset.Instance.swordSprite;
            case ItemType.Helmet: return ItemAsset.Instance.helmetSprite;
            case ItemType.Potion: return ItemAsset.Instance.potionSprite;
        }
    }

    public Item GetObject()
    {
        return this;
    } 

    public void OnAction(Player player)
    {
        switch (itemType)
        {
            default: 
            case ItemType.Armor: if (isWearing == false) { isWearing = true; } else { isWearing = false; }; break ;
            case ItemType.Sword: if (isWearing == false) { isWearing = true; } else { isWearing = false; } break;
            case ItemType.Helmet: if (isWearing == false) { isWearing = true; } else { isWearing = false; };  break;
            case ItemType.Potion: player.UsePotion(); break;
        }
    }

    public Vector2 GetPos()
    {
        return pos;
    }


    public ItemType itemType;
    public bool isWearing = false;
    public int index = 0;
    public Vector2 pos = Vector2.zero;


}
