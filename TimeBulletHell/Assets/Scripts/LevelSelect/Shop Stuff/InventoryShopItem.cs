﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryShopItem", menuName = "ShopItems/InventoryShopItem", order = 1)]
public class InventoryShopItem : AShopItem
{
    [SerializeField]
    protected InventoryShopItems item;

    public override bool onPurchase()
    {
        EventManager.instance.addToInventory(this.enumToItem());
        return true;
    }

    protected virtual InventoryItem enumToItem()
    {
        switch(this.item)
        {
            case InventoryShopItems.TimelessGun:
                return new TimelessGun();
            default:
                return null;
        }
    }
}

public enum InventoryShopItems
{
    TimelessGun
}
