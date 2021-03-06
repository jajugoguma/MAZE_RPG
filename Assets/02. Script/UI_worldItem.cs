﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_worldItem : MonoBehaviour
{
    private WorldItem worldItem;
    [SerializeField]
    private Transform itemtransform = null;
    [SerializeField]
    private Transform itemContainer;

    public void SetWorldItem(WorldItem worldItem_)
    {
        worldItem = worldItem_;
        
    }
    /*
    public void ReSetUI()
    {
        itemtransform = transform.Find("Item");

        foreach(Transform child in itemContainer)
        {
            if (child == transform) continue;
            Destroy(child.gameObject);
        }

        foreach(Item item in worldItem.GetItemList())
        {
            SpriteRenderer img = transform.Find("Item").GetComponent<SpriteRenderer>();
            img.sprite = item.GetSprite();
            Transform transform_ = Instantiate(itemtransform,itemContainer).GetComponent<Transform>();
            transform_.gameObject.SetActive(true);
            //SpriteRenderer img = transform.Find("Item").GetComponent<SpriteRenderer>();
           
            
            transform_.position = item.GetPos();
        }
    }
    */
    public void ReSetUI(Item item)
    {
        itemtransform = transform.Find("Item");
               
        SpriteRenderer img = transform.Find("Item").GetComponent<SpriteRenderer>();
        img.sprite = item.GetSprite();
        Transform transform_ = Instantiate(itemtransform, itemContainer).GetComponent<Transform>();
        transform_.gameObject.SetActive(true);
            //SpriteRenderer img = transform.Find("Item").GetComponent<SpriteRenderer>();


        transform_.position = item.GetPos();
       
    }
}
